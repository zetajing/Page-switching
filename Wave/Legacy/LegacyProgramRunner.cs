using System.Diagnostics;
using System.Text;

namespace Page_switching;

internal static class LegacyProgramRunner
{
    // 原程序通过工作目录内的参数文件传递输入。每个任务使用独立目录。
    internal static async Task RunAsync(string program, string parameterName,
        Func<string, string[]> prepare, string destination, CancellationToken cancellationToken,
        Action<string>? validate = null)
    {
        var settings = LegacyProjectSettings.Load();
        var executable = Path.Combine(settings.ProgramDirectory, program);
        if (!File.Exists(executable)) throw new FileNotFoundException("请在旧设备配置中选择包含 " + program + " 的目录。", executable);
        var fullDestination = Path.GetFullPath(destination);
        Directory.CreateDirectory(Path.GetDirectoryName(fullDestination)!);
        var work = Path.Combine(LegacyProjectSettings.DirectoryPath, "runtime", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(work);
        var output = Path.Combine(work, "output.csv");
        foreach (var source in Directory.EnumerateFiles(settings.ProgramDirectory))
            if (new[] { ".dll", ".dat", ".csv" }.Contains(Path.GetExtension(source).ToLowerInvariant()))
                File.Copy(source, Path.Combine(work, Path.GetFileName(source)), true);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var values = prepare(work);
        // 支持文件可能包含以前的输出，只接受本次进程生成的结果。
        if (File.Exists(output)) File.Delete(output);
        await File.WriteAllTextAsync(Path.Combine(work, parameterName), string.Join("\r\n", values) + "\r\n",
            Encoding.GetEncoding(936), cancellationToken);
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo(executable)
            {
                WorkingDirectory = work, UseShellExecute = false, CreateNoWindow = true,
                RedirectStandardError = true, RedirectStandardOutput = true
            }
        };
        process.Start();
        var errors = process.StandardError.ReadToEndAsync();
        var messages = process.StandardOutput.ReadToEndAsync();
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(TimeSpan.FromMinutes(3));
        try { await process.WaitForExitAsync(timeout.Token); }
        catch (OperationCanceledException)
        {
            if (!process.HasExited) process.Kill(true);
            if (!cancellationToken.IsCancellationRequested) throw new TimeoutException("旧计算程序超过3分钟，已终止。任务目录：" + work);
            throw;
        }
        var diagnostics = (await errors) + (await messages);
        if (process.ExitCode != 0) throw new InvalidOperationException($"{program} 退出码 {process.ExitCode}：{diagnostics}");
        if (!File.Exists(output) || new FileInfo(output).Length == 0)
            throw new InvalidDataException("计算程序没有产生有效输出。任务目录：" + work);
        // 先解析再发布，旧程序报成功但文件损坏时不覆盖用户文件。
        if (validate is null) LegacyWaveSignal.Read(output);
        else validate(output);
        var pending = fullDestination + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try { File.Copy(output, pending); File.Move(pending, fullDestination, true); }
        finally { if (File.Exists(pending)) File.Delete(pending); }
        OperationJournal.Record("旧计算程序", program + " 完成，输出：" + fullDestination);
    }
}
