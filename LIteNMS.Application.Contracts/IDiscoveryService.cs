using Entities.Models;
using LiteNMS.DTOS;
using LiteNMS.DTOS.DeviceDiscovery.UI;

namespace LIteNMS.Application.Contracts;

public interface IDiscoveryService
{
    Task<IEnumerable<DiscoveryResultUIDto>> RunDiscoveryAsync(Guid profileId);
}