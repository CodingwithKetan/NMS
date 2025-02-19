using Entities.Models;

namespace LiteNMS.Infrastructure;

public interface ICredentialProfileRepository : IBaseRepository<CredentialProfile>
{
    Task<bool> ProfileNameExistsAsync(string profileName);
}