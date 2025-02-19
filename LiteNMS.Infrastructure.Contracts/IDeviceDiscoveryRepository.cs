using Entities.Models;

namespace LiteNMS.Infrastructure;

public interface IDeviceDiscoveryRepository : IBaseRepository<DeviceDiscoveryResult>
{
    public Task<IEnumerable<DeviceDiscoveryResult>> GetDiscoveredDevices(Guid deviceProvisionId);
}
