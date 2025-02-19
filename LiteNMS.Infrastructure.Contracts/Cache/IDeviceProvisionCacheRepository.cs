using Entities.Models;

namespace LiteNMS.Infrastructure.Cache;

public interface IDeviceProvisionCacheRepository
{
    void AddOrUpdate(DeviceMetricProvision provision);
    void UpdateProvisionTime(Guid provisionId);
    List<Guid> GetDueProvisions();
    void Remove(Guid provisionId);
    bool IsEmpty();
}