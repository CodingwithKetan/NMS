using Repository.Contract;
using Repository.Contract.Models;

namespace Service.Contract;

public class CredentialProfileService : ICredentialProfileService
{
    private readonly ICredentialProfileRepository _repository;

    public CredentialProfileService(ICredentialProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CredentialProfile>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<CredentialProfile?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<CredentialProfile?> GetByNameAsync(string name)
    {
        return await _repository.GetByNameAsync(name);
    }

    public async Task AddAsync(CredentialProfile profile)
    {
        await _repository.AddAsync(profile);
    }

    public async Task UpdateAsync(CredentialProfile profile)
    {
        await _repository.UpdateAsync(profile);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
