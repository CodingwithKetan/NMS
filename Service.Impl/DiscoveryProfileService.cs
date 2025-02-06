using Repository.Contract;
using Repository.Contract.Models;
using Service.Contract;

namespace Service.Impl;

public class DiscoveryProfileService : IDiscoveryProfileService
{
    private readonly IDiscoveryProfileRepository _repository;

    public DiscoveryProfileService(IDiscoveryProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DiscoveryProfile>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<DiscoveryProfile?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
    public async Task AddAsync(DiscoveryProfile profile) => await _repository.AddAsync(profile);
    public async Task UpdateAsync(DiscoveryProfile profile) => await _repository.UpdateAsync(profile);
    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
}
