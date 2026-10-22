namespace Page_switching;

// 一根轴的操作入口；四个对象共用 AxisService，不各自创建 ADS 连接。
public sealed class Axis
{
    private readonly AxisService _axisService;
    private readonly object _snapshotLock = new object();
    private AxisSnapshot _snapshot;

    // 由轴服务统一创建，轴号和所属服务在创建后保持不变。
    internal Axis(AxisService axisService, int axisNumber)
    {
        _axisService = axisService;
        AxisNumber = axisNumber;
        _snapshot = new AxisSnapshot(axisNumber);
    }

    public int AxisNumber { get; }

    // 最近一次读取结果；连接状态和操作许可仍以共享服务为准。
    public AxisSnapshot Snapshot
    {
        get
        {
            lock (_snapshotLock)
            {
                return _snapshot;
            }
        }
    }

    // 沿用手动页的单轴按钮条件；具体命令仍由服务检查所需变量和控制权。
    public bool CanControl
    {
        get
        {
            return _axisService.CanControlAxis(AxisNumber);
        }
    }

    // 只有共享服务读取完一帧后才替换反馈，页面不直接改轴状态。
    internal void UpdateSnapshot(AxisSnapshot snapshot)
    {
        lock (_snapshotLock)
        {
            _snapshot = snapshot;
        }
    }

    // 只使能本轴。
    public Task EnableAsync(CancellationToken cancellationToken)
    {
        return _axisService.SetAxisEnabledAsync(AxisNumber, true, cancellationToken);
    }

    // 只取消本轴使能。
    public Task DisableAsync(CancellationToken cancellationToken)
    {
        return _axisService.SetAxisEnabledAsync(AxisNumber, false, cancellationToken);
    }

    // 请求本轴报警复位，服务负责置位和清零。
    public Task ResetAlarmAsync(CancellationToken cancellationToken)
    {
        return _axisService.ResetAxisAlarmAsync(AxisNumber, cancellationToken);
    }

    // 请求本轴回零。
    public Task HomeAsync(CancellationToken cancellationToken)
    {
        return _axisService.HomeAxisAsync(AxisNumber, cancellationToken);
    }

    // 请求本轴停止；是否已经停稳仍查看实际反馈。
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _axisService.StopAxisAsync(AxisNumber, cancellationToken);
    }

    // start=true 按下点动，start=false 松开点动；写入顺序由共享服务保持。
    public Task JogAsync(bool positive, bool start, double speed, CancellationToken cancellationToken)
    {
        return _axisService.JogAsync(AxisNumber, positive, start, speed, cancellationToken);
    }
}
