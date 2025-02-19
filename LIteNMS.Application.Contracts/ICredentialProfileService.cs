using Entities.Models;
using LiteNMS.DTOS;

namespace LIteNMS.Application.Contracts;

public interface ICredentialProfileService
{
    Task<IEnumerable<CredentialProfileResponse>> GetAllProfilesAsync();
    Task<CredentialProfileResponse> GetProfileByIdAsync(Guid id);
    Task<Guid> AddProfileAsync(CredentialProfileRequest profile);
    Task UpdateProfileAsync(Guid id, CredentialProfileRequest profile);
    Task DeleteProfileAsync(Guid id);
}
