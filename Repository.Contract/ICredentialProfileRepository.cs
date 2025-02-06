using Repository.Contract.Models;

namespace Repository.Contract;

public interface ICredentialProfileRepository : IRepository<CredentialProfile>
{
    Task<CredentialProfile?> GetByNameAsync(string name);
}
