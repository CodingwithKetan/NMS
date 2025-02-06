using Repository.Contract.Models;

namespace Service.Contract;

public interface IDiscoveryProfileService
{
    Task<IEnumerable<DiscoveryProfile>> GetAllAsync();
    Task<DiscoveryProfile?> GetByIdAsync(int id);
    Task AddAsync(DiscoveryProfile profile);
    Task UpdateAsync(DiscoveryProfile profile);
    Task DeleteAsync(int id);
}
