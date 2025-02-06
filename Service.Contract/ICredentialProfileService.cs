using Repository.Contract.Models;

namespace Service.Contract;

public interface ICredentialProfileService
{
    Task<IEnumerable<CredentialProfile>> GetAllAsync();
    Task<CredentialProfile?> GetByIdAsync(int id);
    Task<CredentialProfile?> GetByNameAsync(string name);
    Task AddAsync(CredentialProfile profile);
    Task UpdateAsync(CredentialProfile profile);
    Task DeleteAsync(int id);
}
