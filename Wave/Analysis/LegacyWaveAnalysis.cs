namespace Page_switching;

internal static class LegacyWaveAnalysis
{
    internal static WaveAnalysisResult Read(string spectrumPath, string elementsPath)
    {
        var rows = File.ReadAllLines(spectrumPath).Where(x => !string.IsNullOrWhiteSpace(x)).Skip(1)
            .Select(WaveSeriesFile.Fields).ToArray();
        if (rows.Length < 2 || rows.Any(x => x.Length < 3))
            throw new InvalidDataException("旧频谱文件需要表头及频率、理论谱、实测谱三列。");
        var f = rows.Select(x => WaveSeriesFile.Number(x[0])).ToArray();
        var theory = rows.Select(x => WaveSeriesFile.Number(x[1])).ToArray();
        var measured = rows.Select(x => WaveSeriesFile.Number(x[2])).ToArray();
        if (f.Any(x => x < 0) || measured.Any(x => x < 0) || theory.Any(x => x < 0) ||
            f.Zip(f.Skip(1), (a, b) => b <= a).Any(x => x))
            throw new InvalidDataException("旧分析结果频率或谱密度无效。");
        var stats = new Dictionary<string, double>();
        var lines = File.ReadAllLines(elementsPath).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        if (lines.Length < 14) throw new InvalidDataException("旧波浪要素文件需要两组标题及12个数值。");
        var names = new[] { "max", "1/100", "1/10", "1/3", "_ave", "min" };
        var unit = lines[0].Contains("(m)", StringComparison.OrdinalIgnoreCase) ? "m" : "旧单位";
        for (var i = 0; i < 6; i++)
        {
            stats["H" + names[i] + " (" + unit + ")"] = WaveSeriesFile.Number(lines[i + 1]);
            stats["TH" + names[i] + " (s)"] = WaveSeriesFile.Number(lines[i + 8]);
        }
        return new WaveAnalysisResult(f, measured, theory, stats, 0, 0);
    }

    internal static async Task<WaveAnalysisResult> AnalyzeAsync(WaveSeries series, double height, double period,
        double depth, double gamma, int spectrum, int passes, int points, CancellationToken token)
    {
        if ((series.Values.Length & (series.Values.Length - 1)) != 0 || series.Values.Length < 64)
            throw new InvalidOperationException("旧分析程序要求至少64点，且样本数为2的幂。");
        WaveAnalysisResult? result = null;
        string? elements = null;
        var destination = Path.Combine(LegacyProjectSettings.DirectoryPath, "analysis", Guid.NewGuid().ToString("N"), "Spectra.dat");
        await LegacyProgramRunner.RunAsync("WaveTJU-6m-11.exe", "SpecAnaParamaters.dat", work =>
        {
            var capture = Path.Combine(work, "measured.txt");
            WaveSeriesFile.WriteLegacy(capture, [series with { Channel = 1 }]);
            elements = Path.Combine(work, "WaveParameters.dat");
            if (File.Exists(elements)) File.Delete(elements);
            return [WaveSeriesFile.Format(height), WaveSeriesFile.Format(period), WaveSeriesFile.Format(depth),
                WaveSeriesFile.Format(gamma), spectrum.ToString(), passes.ToString(), points.ToString(), "1", "1",
                capture, Path.Combine(work, "output.csv"), elements];
        }, destination, token, spectrumFile => result = Read(spectrumFile, elements!));
        File.Copy(elements!, Path.Combine(Path.GetDirectoryName(destination)!, "WaveParameters.dat"));
        return result ?? throw new InvalidDataException("旧分析程序没有有效结果。");
    }
}
