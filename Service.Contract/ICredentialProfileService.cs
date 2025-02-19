using Repository.Contract.Models;

namespace Service.Contract;

public interface ICredentialProfileService
{
    Task<List<CredentialDto>> GetAllCredentialsAsync();
    Task<CredentialDto> GetCredentialByIdAsync(int id);
    Task<CredentialDto> AddCredentialAsync(CredentialDto dto);
    Task<CredentialDto> UpdateCredentialAsync(int id, CredentialDto dto);
    Task<bool> DeleteCredentialAsync(int id);
}
