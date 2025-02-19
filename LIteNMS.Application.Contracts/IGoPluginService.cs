using Entities.Models;
using LiteNMS.DTOS;

namespace LIteNMS.Application.Contracts;

public interface IGoPluginService
{
    Task<IEnumerable<DeviceDiscoveryResultDto>> RunDiscovery(DiscoveryProfile profile);
}