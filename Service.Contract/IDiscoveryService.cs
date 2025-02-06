using LiteNMS.Shared.Dtos;
using Repository.Contract.Models;

namespace Service.Contract;

public interface IDiscoveryService
{
    Task<IEnumerable<Discovery>> FilterAsync(DiscoveryFilterDto filter);
    Task<Discovery?> GetByIdAsync(int id);
    Task RunDiscoveryAsync(int profileId);
}
