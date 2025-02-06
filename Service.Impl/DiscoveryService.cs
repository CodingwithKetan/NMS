using LiteNMS.Shared.Dtos;
using Repository.Contract;
using Repository.Contract.Models;
using Service.Contract;

namespace Service.Impl;

public class DiscoveryService : IDiscoveryService
{
    private readonly IDiscoveryRepository _repository;

    public DiscoveryService(IDiscoveryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<Discovery>> FilterAsync(DiscoveryFilterDto filter)
    {
        if (filter == null)
            throw new ArgumentNullException(nameof(filter));

        return await _repository.FilterDiscoveriesAsync(filter);
    }

    public async Task<Discovery?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid discovery ID.", nameof(id));

        return await _repository.GetByIdAsync(id);
    }

    public async Task RunDiscoveryAsync(int profileId)
    {
        throw new NotImplementedException();
    }
}
