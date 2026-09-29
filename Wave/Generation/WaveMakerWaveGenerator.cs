using System.Globalization;

namespace Page_switching;

// 内置波形算法参考 D:\code\reasonix_project\WaveMaker\src\WaveMaker.Core。
// 它不依赖外部 WFast.exe，输出格式与外部方案保持一致。
internal static class WaveMakerWaveGenerator
{
    private const double Gravity = 9.80665;
    private const int ComponentCount = 512;

    // 按 WaveMaker.Core 的规则波模型生成固定数量的采样点。
    public static IReadOnlyList<double> GenerateRegular(RegularWaveParameters parameters)
    {
        ValidateCommon(parameters.WaterDepth, parameters.TimeStep, parameters.SampleCount);
        if (parameters.Period <= 0 || parameters.WaveHeight < 0)
        {
            throw new InvalidOperationException("规则波周期必须大于 0，波高不能小于 0。");
        }

        var duration = (parameters.SampleCount - 1) * parameters.TimeStep;
        var waveNumber = parameters.TheoryCode == 2
            ? SolveWaveNumber(parameters.Period, parameters.WaterDepth)
            : 0.0;
        var samples = new double[parameters.SampleCount];
        for (var index = 0; index < samples.Length; index++)
        {
            var time = index * parameters.TimeStep;
            var phase = 2.0 * Math.PI * time / parameters.Period;
            samples[index] = parameters.TheoryCode switch
            {
                1 => parameters.WaveHeight / 2.0 * Math.Cos(phase),
                2 => GetStokesValue(parameters.WaveHeight, phase, waveNumber, parameters.WaterDepth),
                3 => GetSolitaryValue(parameters.WaveHeight, parameters.WaterDepth, duration / 2.0, time),
                _ => throw new InvalidOperationException($"不支持的内置规则波理论编号：{parameters.TheoryCode}。")
            };
        }

        return samples;
    }

    // 按 WaveMaker.Core 的频谱叠加方式生成固定种子的随机波形。
    public static IReadOnlyList<double> GenerateIrregular(IrregularWaveParameters parameters)
    {
        ValidateCommon(parameters.WaterDepth, parameters.TimeStep, parameters.SampleCount);
        if (parameters.SignificantPeriod <= 0 || parameters.SignificantHeight <= 0)
        {
            throw new InvalidOperationException("有效周期和有效波高必须大于 0。");
        }

        if (parameters.MinimumPeriod <= 0 || parameters.MaximumPeriod <= 0 ||
            parameters.MinimumPeriod > parameters.MaximumPeriod)
        {
            throw new InvalidOperationException("不规则波周期范围无效。");
        }

        if (parameters.WaveModeCode != 2 || parameters.TheoryCode != 1)
        {
            throw new InvalidOperationException("内置 WaveMaker 方案目前只支持单向线性不规则波，请切换到 WFast.exe。");
        }

        // WaveMaker 的 IrregularWaveGenerator 接收频率范围；这里沿用页面输入的周期范围。
        var frequencyMinimum = 1.0 / parameters.MaximumPeriod;
        var frequencyMaximum = 1.0 / parameters.MinimumPeriod;
        var density = CreateSpectrum(parameters);
        var frequencyInterval = (frequencyMaximum - frequencyMinimum) / ComponentCount;
        var frequencies = new double[ComponentCount];
        var amplitudes = new double[ComponentCount];
        var phases = new double[ComponentCount];
        var random = new SplitMix64(parameters.RandomSeed);

        for (var index = 0; index < ComponentCount; index++)
        {
            var frequency = frequencyMinimum + ((index + 0.5) * frequencyInterval);
            var spectralDensity = density(frequency);
            if (!double.IsFinite(spectralDensity) || spectralDensity < 0)
            {
                throw new InvalidOperationException("内置频谱计算产生了无效谱密度。");
            }

            frequencies[index] = frequency;
            amplitudes[index] = Math.Sqrt(2.0 * spectralDensity * frequencyInterval);
            phases[index] = random.NextUnitInterval() * 2.0 * Math.PI;
        }

        var samples = new double[parameters.SampleCount];
        for (var sampleIndex = 0; sampleIndex < samples.Length; sampleIndex++)
        {
            var time = sampleIndex * parameters.TimeStep;
            var value = 0.0;
            for (var componentIndex = 0; componentIndex < ComponentCount; componentIndex++)
            {
                value += amplitudes[componentIndex] * Math.Cos(
                    (2.0 * Math.PI * frequencies[componentIndex] * time) + phases[componentIndex]);
            }

            samples[sampleIndex] = value;
        }

        return samples;
    }

    // 写出与 WFast 方案相同的 CSV 文件。
    public static void WriteCsv(string path, IReadOnlyList<double> samples, double sampleInterval)
    {
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using var writer = new StreamWriter(path, false, new System.Text.UTF8Encoding(true));
        writer.WriteLine("TimeSeconds,ElevationMetres");
        for (var index = 0; index < samples.Count; index++)
        {
            writer.Write((index * sampleInterval).ToString("R", CultureInfo.InvariantCulture));
            writer.Write(',');
            writer.WriteLine(samples[index].ToString("R", CultureInfo.InvariantCulture));
        }
    }

    // 根据界面选择创建 WaveMaker 频谱函数。
    private static Func<double, double> CreateSpectrum(IrregularWaveParameters parameters) =>
        parameters.SpectrumCode switch
        {
            1 => CreateJonswapSpectrum(parameters.SignificantHeight, parameters.SignificantPeriod, parameters.PeakFactor),
            4 => CreateBretschneiderSpectrum(parameters.SignificantHeight, parameters.SignificantPeriod),
            6 => CreateBretschneiderSpectrum(parameters.SignificantHeight, parameters.SignificantPeriod),
            9 => CreateIttcSpectrum(parameters.SignificantHeight, parameters.SignificantPeriod),
            _ => throw new InvalidOperationException(
                "内置 WaveMaker 方案目前支持 JONSWAP、B 谱、P-M 和 ITTC；其他频谱请切换到 WFast.exe。")
        };

    // 创建 Bretschneider/B 谱函数。
    private static Func<double, double> CreateBretschneiderSpectrum(double height, double period)
    {
        var peakFrequency = 1.0 / period;
        const double beta = 1.25;
        var coefficient = beta * height * height * Math.Pow(peakFrequency, 4) / 4.0;
        return frequency => coefficient * FMinusFiveShape(frequency, peakFrequency, beta);
    }

    // 创建 ITTC 两参数谱函数。
    private static Func<double, double> CreateIttcSpectrum(double height, double period)
    {
        var b = 1.0 / (Math.PI * Math.Pow(period, 4));
        var coefficient = b * height * height / 4.0;
        return frequency =>
        {
            if (frequency <= 0)
            {
                return 0;
            }

            var inverseFourth = 1.0 / Math.Pow(frequency, 4);
            return b * inverseFourth > 745.0
                ? 0
                : coefficient * Math.Exp((-5.0 * Math.Log(frequency)) - (b * inverseFourth));
        };
    }

    // 创建按有效波高归一化的 JONSWAP 谱函数。
    private static Func<double, double> CreateJonswapSpectrum(double height, double period, double gamma)
    {
        if (gamma <= 0)
        {
            throw new InvalidOperationException("JONSWAP 谱峰因子必须大于 0。");
        }

        var peakFrequency = 1.0 / period;
        double Shape(double frequency)
        {
            if (frequency <= 0)
            {
                return 0;
            }

            var baseShape = FMinusFiveShape(frequency, peakFrequency, 1.25);
            var sigma = frequency <= peakFrequency ? 0.07 : 0.09;
            var distance = (frequency - peakFrequency) / (sigma * peakFrequency);
            return baseShape * Math.Pow(gamma, Math.Exp(-0.5 * distance * distance));
        }

        var energy = Integrate(Shape, peakFrequency * 0.01, peakFrequency * 10.0);
        if (!double.IsFinite(energy) || energy <= 0)
        {
            throw new InvalidOperationException("JONSWAP 频谱无法计算有效能量。");
        }

        var scale = height * height / 16.0 / energy;
        return frequency => scale * Shape(frequency);
    }

    // 返回 f^-5 指数谱的基础形状。
    private static double FMinusFiveShape(double frequency, double characteristicFrequency, double beta)
    {
        if (frequency <= 0)
        {
            return 0;
        }

        var ratio = characteristicFrequency / frequency;
        var ratioToFourth = Math.Pow(ratio, 4);
        return !double.IsFinite(ratioToFourth) || beta * ratioToFourth > 745.0
            ? 0
            : Math.Exp((-5.0 * Math.Log(frequency)) - (beta * ratioToFourth));
    }

    // 用辛普森法计算频谱总能量。
    private static double Integrate(Func<double, double> function, double minimum, double maximum)
    {
        const int intervals = 2000;
        var step = (maximum - minimum) / intervals;
        var total = function(minimum) + function(maximum);
        for (var index = 1; index < intervals; index++)
        {
            total += (index % 2 == 0 ? 2.0 : 4.0) * function(minimum + index * step);
        }

        return total * step / 3.0;
    }

    // 计算二阶 Stokes 波在指定相位的高程。
    private static double GetStokesValue(double height, double phase, double waveNumber, double depth)
    {
        var amplitude = height / 2.0;
        var sigma = Math.Tanh(waveNumber * depth);
        var secondOrder = waveNumber * amplitude * amplitude * (3.0 - sigma * sigma) /
                          (4.0 * sigma * sigma * sigma);
        return amplitude * Math.Cos(phase) + secondOrder * Math.Cos(2.0 * phase);
    }

    // 计算孤立波在指定时间的高程。
    private static double GetSolitaryValue(double height, double depth, double peakTime, double time)
    {
        var celerity = Math.Sqrt(Gravity * (depth + height));
        var alpha = Math.Sqrt((3.0 * height) / (4.0 * Math.Pow(depth, 3)));
        var sech = 1.0 / Math.Cosh(alpha * celerity * (time - peakTime));
        return height * sech * sech;
    }

    // 用二分法求解线性色散关系中的波数。
    private static double SolveWaveNumber(double period, double depth)
    {
        var omega = 2.0 * Math.PI / period;
        var lower = 0.0;
        var upper = Math.Max(omega * omega / Gravity, omega / Math.Sqrt(Gravity * depth));
        while (Gravity * upper * Math.Tanh(upper * depth) < omega * omega)
        {
            upper *= 2.0;
        }

        for (var iteration = 0; iteration < 100; iteration++)
        {
            var current = (lower + upper) / 2.0;
            var value = Gravity * current * Math.Tanh(current * depth) - omega * omega;
            if (Math.Abs(value) < 1e-12 * omega * omega)
            {
                return current;
            }

            if (value > 0)
            {
                upper = current;
            }
            else
            {
                lower = current;
            }
        }

        return (lower + upper) / 2.0;
    }

    // 校验规则波和不规则波共用的采样参数。
    private static void ValidateCommon(double depth, double interval, int sampleCount)
    {
        if (depth <= 0 || interval <= 0 || sampleCount < 2)
        {
            throw new InvalidOperationException("水深、采样间隔必须大于 0，采样点数至少为 2。");
        }
    }

    private sealed class SplitMix64
    {
        private ulong _state;

        // 用整数种子初始化可重复的伪随机序列。
        public SplitMix64(int seed)
        {
            _state = unchecked((ulong)(uint)seed) + 0x9E3779B97F4A7C15UL;
        }

        // 生成 [0,1) 范围内的下一个随机数。
        public double NextUnitInterval()
        {
            _state += 0x9E3779B97F4A7C15UL;
            var value = _state;
            value = (value ^ (value >> 30)) * 0xBF58476D1CE4E5B9UL;
            value = (value ^ (value >> 27)) * 0x94D049BB133111EBUL;
            value ^= value >> 31;
            return (value >> 11) * (1.0 / (1UL << 53));
        }
    }
}
