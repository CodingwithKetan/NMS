using LiteNMS.Shared.Dtos;
using Repository.Contract.Models;

namespace Repository.Contract;

public interface IDiscoveryRepository : IRepository<Discovery>
{
    Task<IEnumerable<Discovery>> FilterDiscoveriesAsync(DiscoveryFilterDto filter);
}
