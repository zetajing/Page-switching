using System.Globalization;

namespace Page_switching;

// 预检使用上传前读取的四轴参数快照；记录创建后不再改变，不反写 PLC 配置。
public sealed record AxisTrajectoryLimits(double MinPosition, double MaxPosition, double MaxVelocity,
    double MaxAcceleration, double StopDeceleration, double StartTolerance, double InitVelocity);

internal static class WavePointValidator
{
    private const double Dt = 0.002;

    // 按 PLC 原有整表数值规则检查最终轴点位；不缩放、不平滑、不补点，也不修改输入数组。
    internal static void Validate(double[] points, IReadOnlyList<AxisTrajectoryLimits> configs, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        if (points is null || points.Length < 4)
            throw new InvalidDataException("轴 1～4 共用点位，点号 0（从 0 起），时间 0 秒：点位数量不合格；实际值 "
                + (points?.Length ?? 0) + "；限值至少 4 点。");
        if (configs is null || configs.Count != 4)
            throw new InvalidDataException("轴 1～4，点号 0（从 0 起），时间 0 秒：轴配置数量不合格；实际值 "
                + (configs?.Count ?? 0) + "；限值 4 轴。");

        for (int axis = 1; axis <= 4; axis++)
        {
            var config = configs[axis - 1];
            if (config is null)
                throw new InvalidDataException($"轴 {axis}，点号 0（从 0 起），时间 0 秒：轴配置不合格；实际值为空；限值为完整的七项轴参数。");
            // 固定参数每轴只检查一次；正向范围判断与原 PLC 门槛一致，拒绝 NaN 和无穷。
            if (!double.IsFinite(config.MinPosition) || config.MinPosition < -1.0E12)
                throw Failure(axis, 0, "最小位置配置", config.MinPosition, ">= -1000000000000，且为有限值");
            if (!double.IsFinite(config.MaxPosition) || config.MaxPosition > 1.0E12)
                throw Failure(axis, 0, "最大位置配置", config.MaxPosition, "<= 1000000000000，且为有限值");
            if (config.MinPosition >= config.MaxPosition)
                throw Failure(axis, 0, "最小位置配置", config.MinPosition, "< 最大位置 " + Number(config.MaxPosition));
            if (!(config.MaxVelocity > 0 && config.MaxVelocity < 1.0E12))
                throw Failure(axis, 0, "最大速度配置", config.MaxVelocity, "> 0 且 < 1000000000000");
            if (!(config.MaxAcceleration > 0 && config.MaxAcceleration < 1.0E12))
                throw Failure(axis, 0, "最大加速度配置", config.MaxAcceleration, "> 0 且 < 1000000000000");
            if (!(config.StopDeceleration > 0 && config.StopDeceleration <= config.MaxAcceleration))
                throw Failure(axis, 0, "停止减速度配置", config.StopDeceleration,
                    "> 0 且 <= 最大加速度 " + Number(config.MaxAcceleration));
            if (!(config.StartTolerance > 0 && config.StartTolerance < 1.0E6))
                throw Failure(axis, 0, "首点到位容差配置", config.StartTolerance, "> 0 且 < 1000000");
            if (!(config.InitVelocity > 0 && config.InitVelocity <= config.MaxVelocity))
                throw Failure(axis, 0, "首点定位速度配置", config.InitVelocity,
                    "> 0 且 <= 最大速度 " + Number(config.MaxVelocity));

            // 首两点、末两点分别相等，首末点给定速度与加速度均为零。
            if (points[0] != points[1])
                throw Failure(axis, 1, "首两点静止", points[1], "等于第 0 点 " + Number(points[0]));
            if (points[^2] != points[^1])
                throw Failure(axis, points.Length - 1, "末两点静止", points[^1],
                    "等于第 " + (points.Length - 2) + " 点 " + Number(points[^2]));

            for (int point = 0; point < points.Length; point++)
            {
                if ((point & 1023) == 0) token.ThrowIfCancellationRequested();
                double position = points[point];
                double velocity = 0;
                double acceleration = 0;
                if (point > 0 && point < points.Length - 1)
                {
                    // 运算顺序与 F_TrajectoryPoint 一致；保持原精度，不能加容差掩盖超限。
                    velocity = (points[point + 1] - points[point - 1]) / (2 * Dt);
                    acceleration = (points[point + 1] - 2 * position + points[point - 1]) / (Dt * Dt);
                }
                if (!double.IsFinite(position) || position < config.MinPosition || position > config.MaxPosition)
                    throw Failure(axis, point, "位置", position,
                        "[" + Number(config.MinPosition) + ", " + Number(config.MaxPosition) + "]，且为有限值");
                if (!double.IsFinite(velocity) || Math.Abs(velocity) > config.MaxVelocity)
                    throw Failure(axis, point, "差分速度", velocity, "绝对值 <= " + Number(config.MaxVelocity));
                if (!double.IsFinite(acceleration) || Math.Abs(acceleration) > config.MaxAcceleration)
                    throw Failure(axis, point, "差分加速度", acceleration,
                        "绝对值 <= " + Number(config.MaxAcceleration));
                if (point > 0 && Math.Abs(position - points[point - 1]) > config.MaxVelocity * Dt)
                    throw Failure(axis, point, "相邻点位差", Math.Abs(position - points[point - 1]),
                        "<= 最大速度 × 0.002 = " + Number(config.MaxVelocity * Dt));

                // 原停车估算含一拍余量；这里只迁移数值检查，不增加机械换算或现场能力假设。
                double stopPosition = position + velocity * Math.Abs(velocity) / (2 * config.StopDeceleration) + velocity * Dt;
                if (!double.IsFinite(stopPosition) || stopPosition < config.MinPosition || stopPosition > config.MaxPosition)
                    throw Failure(axis, point, "预测停车位置", stopPosition,
                        "[" + Number(config.MinPosition) + ", " + Number(config.MaxPosition) + "]，且为有限值");
            }
        }
    }

    private static InvalidDataException Failure(int axis, int point, string item, double actual, string limit) =>
        new($"轴 {axis}，点号 {point}（从 0 起），时间 {Number(point * Dt)} 秒：{item}不合格；实际值 {Number(actual)}；限值 {limit}。");

    private static string Number(double value) => value.ToString("G17", CultureInfo.InvariantCulture);
}
