namespace Page_switching;

// 页面和操作日志共用的只读说明；不读取额外 ADS 符号，不修改任何 PLC 条件。
internal static class PlcWaveDiagnostics
{
    // 与 PLC MAIN / FB_FourAxisTrajectory 的来源码对应，原始 MC 错误号另行显示。
    internal static string DescribeSource(ushort source) => source switch
    {
        0 => "组条件，详见原始错误码",
        1 => "运行许可丢失",
        2 => "控制权失效",
        3 => "使能请求撤销或使能反馈丢失",
        4 => "单轴停止请求",
        5 => "点动、回原或复位命令冲突",
        6 => "软限位或硬限位触发",
        7 => "轨迹心跳超时",
        8 => "轴故障或 NC I/O 无效",
        9 => "外部给定意外退出",
        10 => "MC_MoveAbsolute 首点定位",
        11 => "MC_ExtSetPointGenEnable 开启外部给定",
        12 => "MC_Stop 首点定位停车",
        13 => "MC_ExtSetPointGenDisable 关闭外部给定",
        14 => "等待退出超时",
        15 => "轴参数检查",
        16 => "轴就绪或首点到位检查",
        17 => "收段、拼表或启动元数据检查",
        18 => "拼表内存复制",
        19 => "首点定位超时",
        20 => "开启外部给定超时",
        _ => $"未知来源 {source}"
    };

    internal static string DescribeFirstFault(PlcMachineMonitor frame)
    {
        if (!frame.Error) return "无轨迹组故障";
        string axis = frame.FaultAxis == 0 ? "组条件" : $"轴 {frame.FaultAxis}";
        return $"首次故障：{DescribeState(frame.FaultState)}，{axis}，{DescribeSource(frame.FaultSource)}；"
            + $"原始错误={frame.TrajectoryErrorId}（0x{frame.TrajectoryErrorId:X}）；"
            + $"停车/退出错误={frame.CleanupErrorId}（0 表示未报告清理错误，不代表已经停稳）";
    }

    // 对应 FB_FourAxisTrajectory 的 nState，不是页面的 WaveState 枚举。
    internal static string DescribeState(ushort state)
    {
        switch (state)
        {
            case 0: return "空闲";
            case 5: return "拼接点位表";
            case 10: return "检查启动条件";
            case 15: return "定位到首点";
            case 20: return "开启外部给定";
            case 30: return "执行轨迹";
            case 40: return "保持停止位置";
            case 50: return "减速停止";
            case 60: return "关闭外部给定";
            case 70: return "等待退出完成";
            case 90: return "故障等待复位";
            default: return "未知状态";
        }
    }

    // 1003/1005 保留兼容旧 PLC；新版整表数值检查由 WavePointValidator 在下发前完成。
    internal static string DescribeFault(uint code)
    {
        string reason;
        switch (code)
        {
            case 0: return "无轨迹故障";
            case 1001: reason = "点数、2 ms 周期、分段偏移或上传序号不符合要求，或点位表尚未收齐"; break;
            case 1002: reason = "轴配置无效，请检查 FB_AxisConfig 和 GVL_motion.aConfig[1..4]"; break;
            case 1003: reason = "点位校验失败：检查首尾静止、数值有效性、位置/速度/加速度上限及停车空间"; break;
            case 1004: reason = "轴未就绪，或首点定位的实际/设定位置偏差超过到位容差"; break;
            case 1005: reason = "相邻点位差超过速度上限 × 0.002 秒"; break;
            case 1006: reason = "轨迹心跳超时，请检查上位机通信和任务状态"; break;
            case 1007: reason = "运行联锁中断、手动命令冲突或外部给定意外退出"; break;
            case 1008: reason = "开启外部给定超时"; break;
            case 1009: reason = "关闭外部给定或停止块退出超时"; break;
            case 1010: reason = "轴故障或 NC I/O 反馈无效"; break;
            case 1011: reason = "定位到首点超时"; break;
            case 1012: reason = "点位表内存复制字节数异常"; break;
            default:
                reason = $"未映射的故障码（0x{code:X}），请在 TwinCAT 检查相关 MC 块的 ErrorID";
                break;
        }
        return $"PLC 轨迹故障 {code}：{reason}。";
    }

    // CurrentPoint 是本扫描准备发送的轨迹点，不表示轴已到位，也不是整表校验失败点。
    internal static string DescribeFrame(PlcMachineMonitor frame)
    {
        return DescribeFirstFault(frame) + "；"
            + $"PLC 程序={frame.ProgramVersion}，接口行为版本={frame.BehaviorVersion}，"
            + $"PLC 状态={frame.TrajectoryState}（{DescribeState(frame.TrajectoryState)}），"
            + $"执行点={frame.CurrentPoint}/{frame.PointCount}，"
            + $"接收={frame.ReceivedCount}/{frame.PointCount}，拼接={frame.LoadedCount}/{frame.PointCount}，"
            + $"序号（接收/执行）={frame.ReceivedSequence}/{frame.AcceptedSequence}，"
            + $"控制端={frame.ControlOwner}，上传站={frame.UploadStation}，"
            + $"许可={frame.MotionPermit}，配置有效={frame.ConfigValid}，轴故障={frame.AxisFault}，"
            + $"轴空闲={frame.AxesIdle}，轴停稳={frame.AxesStopped}，"
            + $"使能轴={AxisNumbers(frame.Axis1.Enabled, frame.Axis2.Enabled, frame.Axis3.Enabled, frame.Axis4.Enabled)}，"
            + $"已回原轴={AxisNumbers(frame.Axis1.Homed, frame.Axis2.Homed, frame.Axis3.Homed, frame.Axis4.Homed)}，"
            + $"报警轴={AxisNumbers(frame.Axis1.Alarm, frame.Axis2.Alarm, frame.Axis3.Alarm, frame.Axis4.Alarm)}";
    }

    private static string AxisNumbers(bool axis1, bool axis2, bool axis3, bool axis4)
    {
        var numbers = new List<int>();
        if (axis1) numbers.Add(1);
        if (axis2) numbers.Add(2);
        if (axis3) numbers.Add(3);
        if (axis4) numbers.Add(4);
        return numbers.Count == 0 ? "无" : string.Join(",", numbers);
    }
}
