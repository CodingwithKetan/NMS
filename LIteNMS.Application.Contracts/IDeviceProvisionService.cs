using LiteNMS.DTOS;

namespace LIteNMS.Application.Contracts;

public interface IDeviceProvisionService
{
    public Task<Guid> ProvisionDevices(DeviceProvisionRequestDto request);
}
