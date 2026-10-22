using InduLink.Abstractions;
using InduLink.Protocols.Ads;

namespace Page_switching;

// 手动页反馈读取：读取并校验整帧，更新四轴快照；不申请控制权，不写操作命令。
public sealed partial class AxisService
{
    // 读取四根轴的当前位置、速度和状态，供手动页面刷新显示。
    public async Task<IReadOnlyList<AxisSnapshot>> ReadSnapshotAsync(CancellationToken cancellationToken)
    {
        ThrowIfDisposed();

        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient();
            IReadOnlyList<AxisSnapshot> snapshots;
            if (client is null)
            {
                snapshots = CreateUnavailableSnapshots("ADS 未连接");
            }
            else
            {
                snapshots = await ReadAdsSnapshotsAsync(client, cancellationToken).ConfigureAwait(false);
            }
            UpdateAxisSnapshots(snapshots);
            return snapshots;
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            lock (_stateSync)
            {
                _lastError = ex.Message;
            }

            var snapshots = CreateUnavailableSnapshots("读取失败");
            UpdateAxisSnapshots(snapshots);
            return snapshots;
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 使用现有 DLL 一次读取四轴反馈，布局校验通过后才更新手动页。
    // 控制命令仍逐项写本站邮箱，不整体回写反馈结构体。
    private async Task<IReadOnlyList<AxisSnapshot>> ReadAdsSnapshotsAsync(
        AdsClient client,
        CancellationToken cancellationToken)
    {
        var frame = await client.ReadAnyAsync<PlcMachineMonitor>(
            _options.SnapshotSymbol, cancellationToken: cancellationToken).ConfigureAwait(false);
        return frame.ToAxisSnapshots();
    }

    // 在未连接或读取失败时，生成四根轴的占位状态。
    private IReadOnlyList<AxisSnapshot> CreateUnavailableSnapshots(string status)
    {
        return Enumerable.Range(1, 4)
            .Select(index =>
            {
                var snapshot = new AxisSnapshot(index) { StatusText = status };
                return snapshot;
            })
            .ToArray();
    }

    // 仍然整帧读取四轴，再把各轴结果交给固定对象，避免逐轴增加 ADS 请求。
    private void UpdateAxisSnapshots(IReadOnlyList<AxisSnapshot> snapshots)
    {
        for (int index = 0; index < Axes.Count; index++)
        {
            Axes[index].UpdateSnapshot(snapshots[index]);
        }
    }
}
