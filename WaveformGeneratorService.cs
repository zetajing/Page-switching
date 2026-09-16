using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Page_switching;

// 保存一次规则波生成所需的全部参数。
public sealed record RegularWaveParameters(
    double WaterDepth,
    double Period,
    double WaveHeight,
    double Direction,
    double TimeStep,
    int SampleCount,
    double CharacteristicFrequency,
    double CharacteristicPeriod,
    int TheoryCode,
    int SideCode);

// 保存一次不规则波生成所需的全部参数。
public sealed record IrregularWaveParameters(
    double WaterDepth,
    double SignificantPeriod,
    double SignificantHeight,
    double Direction,
    double TimeStep,
    int SampleCount,
    double CharacteristicFrequency,
    double CharacteristicPeriod,
    double PeakFactor,
    int RandomSeed,
    double MinimumPeriod,
    double MaximumPeriod,
    double MinimumDifferencePeriod,
    double MaximumDifferencePeriod,
    double NegativeDirection,
    double PositiveDirection,
    int WaveModeCode,
    int SpectrumCode,
    int TheoryCode,
    int SideCode);

public sealed class WaveformGenerationResult
{
    public required string OutputPath { get; init; }
    public required IReadOnlyList<double> Samples { get; init; }
    public required TimeSpan Elapsed { get; init; }
}

public sealed class WaveformGeneratorOptions
{
    public string WFastPath { get; init; } = string.Empty;

    // 从 App.config 读取外部波形生成程序路径和执行超时时间。
    public static WaveformGeneratorOptions FromConfiguration() => new()
    {
        WFastPath = ConfigurationManager.AppSettings["WaveGeneratorPath"]?.Trim() ?? string.Empty
    };
}

public sealed class WaveformGeneratorService : IDisposable
{
    private readonly WaveformGeneratorOptions _options;
    private readonly SemaphoreSlim _operationGate = new(1, 1);
    private Process? _runningProcess;
    private bool _disposed;

    // 保存波形生成配置，供后续生成任务使用。
    public WaveformGeneratorService(WaveformGeneratorOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    // 根据规则波参数生成输入文件，并调用外部程序生成波形。
    public Task<WaveformGenerationResult> GenerateRegularAsync(
        RegularWaveParameters parameters,
        string outputPath,
        CancellationToken cancellationToken) =>
        GenerateAsync(BuildRegularParameterFile(parameters, outputPath), outputPath, cancellationToken);

    // 根据不规则波参数生成输入文件，并调用外部程序生成波形。
    public Task<WaveformGenerationResult> GenerateIrregularAsync(
        IrregularWaveParameters parameters,
        string outputPath,
        CancellationToken cancellationToken) =>
        GenerateAsync(BuildIrregularParameterFile(parameters, outputPath), outputPath, cancellationToken);

    // 启动外部生成程序，等待输出文件并读取生成结果。
    private async Task<WaveformGenerationResult> GenerateAsync(
        string parameterFile,
        string outputPath,
        CancellationToken cancellationToken)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        var configuredPath = ConfigurationManager.AppSettings["WaveGeneratorPath"]?.Trim();
        var wFastPath = string.IsNullOrWhiteSpace(configuredPath)
            ? _options.WFastPath
            : configuredPath;
        if (string.IsNullOrWhiteSpace(wFastPath))
        {
            throw new InvalidOperationException("请先在配置页面选择并保存 WFast.exe 路径。");
        }

        var generatorPath = Path.GetFullPath(wFastPath);
        if (!File.Exists(generatorPath))
        {
            throw new FileNotFoundException("找不到配置的 WFast.exe 文件。", generatorPath);
        }

        if (string.IsNullOrWhiteSpace(outputPath) || outputPath.Contains('\r') || outputPath.Contains('\n'))
        {
            throw new InvalidOperationException("波形保存路径不能为空，且不能包含换行符。");
        }

        var fullOutputPath = Path.GetFullPath(outputPath);
        var outputDirectory = Path.GetDirectoryName(fullOutputPath);
        if (string.IsNullOrWhiteSpace(outputDirectory))
        {
            throw new InvalidOperationException("波形保存路径无效。");
        }

        Directory.CreateDirectory(outputDirectory);
        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);

        string? workDirectory = null;
        try
        {
            workDirectory = Path.Combine(
                AppContext.BaseDirectory,
                "wave-runtime",
                Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(workDirectory);
            CopyGeneratorSupportFiles(generatorPath, workDirectory);
            var basicParametersPath = Path.Combine(workDirectory, "BasicParameters.dat");
            await File.WriteAllTextAsync(
                basicParametersPath,
                parameterFile,
                Encoding.Default,
                cancellationToken).ConfigureAwait(false);

            var stopwatch = Stopwatch.StartNew();
            var startInfo = new ProcessStartInfo
            {
                FileName = generatorPath,
                WorkingDirectory = workDirectory,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true };
            if (!process.Start())
            {
                throw new InvalidOperationException("无法启动 WFast.exe。");
            }

            _runningProcess = process;
            var standardOutputTask = process.StandardOutput.ReadToEndAsync(cancellationToken);
            var standardErrorTask = process.StandardError.ReadToEndAsync(cancellationToken);
            using var timeoutCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCancellation.CancelAfter(TimeSpan.FromMinutes(2));

            try
            {
                await process.WaitForExitAsync(timeoutCancellation.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                TryKill(process);
                if (!cancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException("WFast.exe 生成超过 120 秒，已终止进程。");
                }

                throw;
            }

            var standardOutput = await standardOutputTask.ConfigureAwait(false);
            var standardError = await standardErrorTask.ConfigureAwait(false);
            stopwatch.Stop();

            if (process.ExitCode != 0)
            {
                var details = string.IsNullOrWhiteSpace(standardError)
                    ? standardOutput
                    : standardError;
                throw new InvalidOperationException(
                    $"WFast.exe 生成失败，退出码 {process.ExitCode}。{details}");
            }

            if (!File.Exists(fullOutputPath) || new FileInfo(fullOutputPath).Length == 0)
            {
                throw new InvalidOperationException("WFast.exe 已结束，但没有生成有效的 CSV 文件。");
            }

            var samples = WaveformCsvReader.ReadSamples(fullOutputPath);
            if (samples.Count < 2)
            {
                throw new InvalidOperationException("CSV 中没有读取到足够的波形数据。");
            }

            return new WaveformGenerationResult
            {
                OutputPath = fullOutputPath,
                Samples = samples,
                Elapsed = stopwatch.Elapsed
            };
        }
        finally
        {
            _runningProcess = null;
            if (workDirectory is not null)
            {
                try
                {
                    Directory.Delete(workDirectory, recursive: true);
                }
                catch
                {
                    // 临时目录清理失败不应覆盖生成结果或原始异常。
                }
            }
        }
    }

    // 将生成程序同目录下的依赖文件复制到本次临时工作目录。
    private static void CopyGeneratorSupportFiles(string generatorPath, string workDirectory)
    {
        var generatorDirectory = Path.GetDirectoryName(generatorPath);
        if (string.IsNullOrWhiteSpace(generatorDirectory) || !Directory.Exists(generatorDirectory))
        {
            return;
        }

        // 旧版 WFast 会从当前工作目录读取造风/造流系数表；临时目录需要带上这些只读数据文件。
        foreach (var supportFile in Directory.EnumerateFiles(generatorDirectory, "*.csv"))
        {
            var targetPath = Path.Combine(workDirectory, Path.GetFileName(supportFile));
            File.Copy(supportFile, targetPath, overwrite: true);
        }
    }

    // 按外部程序要求组装规则波参数文件内容。
    private static string BuildRegularParameterFile(RegularWaveParameters p, string outputPath)
    {
        var values = new[]
        {
            Format(p.WaterDepth),
            Format(p.Period),
            Format(p.WaveHeight),
            Format(p.Direction),
            "0.1",
            "10",
            "0",
            "0",
            "1",
            "0",
            "0",
            "0",
            "0",
            Format(p.TimeStep),
            p.SampleCount.ToString(CultureInfo.InvariantCulture),
            Format(p.CharacteristicFrequency),
            Format(p.CharacteristicPeriod),
            p.TheoryCode.ToString(CultureInfo.InvariantCulture),
            "1",
            p.SideCode.ToString(CultureInfo.InvariantCulture),
            "W2",
            outputPath
        };

        return string.Join("\r\n", values) + "\r\n";
    }

    // 按外部程序要求组装不规则波参数文件内容。
    private static string BuildIrregularParameterFile(IrregularWaveParameters p, string outputPath)
    {
        var values = new[]
        {
            Format(p.SignificantPeriod),
            Format(p.SignificantHeight),
            Format(p.WaterDepth),
            Format(p.Direction),
            Format(p.MinimumPeriod),
            Format(p.MaximumPeriod),
            Format(p.NegativeDirection),
            Format(p.PositiveDirection),
            p.WaveModeCode.ToString(CultureInfo.InvariantCulture),
            p.SpectrumCode.ToString(CultureInfo.InvariantCulture),
            Format(p.PeakFactor),
            p.RandomSeed.ToString(CultureInfo.InvariantCulture),
            "2",
            Format(p.TimeStep),
            p.SampleCount.ToString(CultureInfo.InvariantCulture),
            Format(p.CharacteristicFrequency),
            Format(p.CharacteristicPeriod),
            "0",
            p.TheoryCode.ToString(CultureInfo.InvariantCulture),
            p.SideCode.ToString(CultureInfo.InvariantCulture),
            "W2",
            outputPath
        };

        return string.Join("\r\n", values) + "\r\n";
    }

    // 使用固定小数格式输出参数，避免系统区域设置影响文件内容。
    private static string Format(double value) =>
        value.ToString("0.###############", CultureInfo.InvariantCulture);

    // 超时或取消时尝试终止外部生成进程。
    private static void TryKill(Process process)
    {
        try
        {
            if (!process.HasExited)
            {
                process.Kill(entireProcessTree: true);
            }
        }
        catch
        {
            // 进程已经结束或系统拒绝终止时，不覆盖原始取消异常。
        }
    }

    // 取消正在执行的生成任务并释放取消令牌。
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        if (_runningProcess is not null)
        {
            TryKill(_runningProcess);
        }

        _operationGate.Dispose();
    }
}

public static class WaveformCsvReader
{
    // 从外部程序输出文件中读取所有有效波形采样值。
    public static IReadOnlyList<double> ReadSamples(string path)
    {
        var samples = new List<double>();
        foreach (var line in File.ReadLines(path))
        {
            var values = line.Split(',', ';', '\t')
                .Select(value => TryParse(value, out var number) ? number : (double?)null)
                .Where(value => value.HasValue)
                .Select(value => value!.Value)
                .ToArray();

            if (values.Length == 0)
            {
                continue;
            }

            samples.Add(values.Length == 1 ? values[0] : values[^1]);
        }

        return samples;
    }

    // 使用不受系统区域设置影响的方式解析数值文本。
    private static bool TryParse(string value, out double number) =>
        double.TryParse(value.Trim(), NumberStyles.Float | NumberStyles.AllowThousands,
            CultureInfo.InvariantCulture, out number) ||
        double.TryParse(value.Trim(), NumberStyles.Float | NumberStyles.AllowThousands,
            CultureInfo.CurrentCulture, out number);
}
