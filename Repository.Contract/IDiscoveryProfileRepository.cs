using Repository.Contract.Models;

namespace Repository.Contract;

public interface IDiscoveryProfileRepository : IRepository<DiscoveryProfile>
{
    Task<DiscoveryProfile?> GetByNameAsync(string name);
}
