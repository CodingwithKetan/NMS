
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using LIteNMS.Application.Contracts;
using LiteNMS.DTOS;
using LiteNMS.Infrastructure;
using LiteNMS.Utils.Security;

namespace LiteNMS.Services
{
    public class CredentialProfileService : ICredentialProfileService
    {
        private readonly ICredentialProfileRepository _repository;

        public CredentialProfileService(ICredentialProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CredentialProfileResponse>> GetAllProfilesAsync()
        {
            var credentialProfiles = await _repository.GetAllAsync();
            return credentialProfiles.Select(ToCredentialProfileResponse);
        }

        public async Task<CredentialProfileResponse> GetProfileByIdAsync(Guid id)
        {
            var credentialProfile = await _repository.GetByIdAsync(id);
            return credentialProfile != null ? ToCredentialProfileResponse(credentialProfile) : null;
        }


        public async Task<Guid> AddProfileAsync(CredentialProfileRequest credentialProfileRequest)
        {
            bool exists = await _repository.ProfileNameExistsAsync(credentialProfileRequest.ProfileName);
            if (exists)
            {
                throw new InvalidOperationException("A Credential Profile with this name already exists.");
            }

            var credentialProfile = ToCredentialProfile(credentialProfileRequest);
            await _repository.AddAsync(credentialProfile);

            return credentialProfile.Id; // Return the ID of the created profile
        }


        public async Task UpdateProfileAsync(Guid id, CredentialProfileRequest credentialProfileRequest)
        {
            var profile = await GetProfileByIdAsync(id);
            if (profile == null)
            {
                throw new InvalidOperationException("A Credential Profile with this id does not exist.");
            }
            await _repository.UpdateAsync(ToCredentialProfile(id, credentialProfileRequest));
        }

        public async Task DeleteProfileAsync(Guid id) => await _repository.DeleteAsync(id);

        private CredentialProfileResponse ToCredentialProfileResponse(CredentialProfile credentialProfile)
        {
            return new CredentialProfileResponse()
            {
                Id = credentialProfile.Id,
                ProfileName = credentialProfile.ProfileName,
                Username = credentialProfile.Username,
                CreatedAt = credentialProfile.CreatedAt,
            };
        }

        private CredentialProfile ToCredentialProfile(Guid id, CredentialProfileRequest credentialProfileRequest)
        {
            var profile = ToCredentialProfile(credentialProfileRequest);
            profile.Id = id;
            return profile;
        }
        
        private CredentialProfile ToCredentialProfile(CredentialProfileRequest credentialProfileRequest)
        {
            return new CredentialProfile()
            {
                Id = Guid.NewGuid(),
                ProfileName =  credentialProfileRequest.ProfileName,
                Username = credentialProfileRequest.Username,
                EncryptedPassword =  EncryptionHelper.Encrypt(credentialProfileRequest.Password, "your-secure-32char-key-123456789")
            };
        }
    }
}