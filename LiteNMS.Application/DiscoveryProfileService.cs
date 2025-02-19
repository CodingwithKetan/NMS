using Entities.Models;
using LIteNMS.Application.Contracts;
using LiteNMS.DTOS;
using LiteNMS.Infrastructure;

namespace LiteNMS.Services;

public class DiscoveryProfileService : IDiscoveryProfileService
{
    private readonly IDiscoveryProfileRepository _repository;
    public DiscoveryProfileService(IDiscoveryProfileRepository repository) => _repository = repository;

    public async Task<IEnumerable<DiscoveryProfileResponseDto>> GetAllAsync()
    {
        var profiles = await _repository.GetAllAsync();
        return profiles.Select(ToDiscoveryProfileResponseDto);
    }

    public async Task<DiscoveryProfileResponseDto> GetByIdAsync(Guid id)
    { 
        var profile = await _repository.GetByIdAsync(id);
        return profile != null ? ToDiscoveryProfileResponseDto(profile) : null;
    }

    public async Task<Guid> AddAsync(DiscoveryProfileRequestDto profileDto)
    {
        var profile = ToDiscoveryProfile(profileDto);
        await _repository.AddAsync(profile);
        return profile.Id;
    }
    
    public async Task UpdateAsync(Guid id, DiscoveryProfileRequestDto profileDto)
    {
        var profile = await _repository.GetByIdAsync(id);
        if (profile == null) return;
        profile = ToDiscoveryProfile(profileDto);
        await _repository.UpdateAsync(profile);
    }
    public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    
    private static DiscoveryProfile ToDiscoveryProfile(DiscoveryProfileRequestDto profileDto)
    {
        var profile = new DiscoveryProfile
        {
            Id = Guid.NewGuid(),
            Name = profileDto.Name,
            IPRanges = profileDto.IpRanges,
            CredentialProfileIds = profileDto.CredentialProfileIds,
            Port = profileDto.Port,
        };
        return profile;
    }

    private static DiscoveryProfileResponseDto ToDiscoveryProfileResponseDto(DiscoveryProfile profile)
    {
        return new DiscoveryProfileResponseDto()
        {
            DiscoveryProfileId =  profile.Id,
            Name = profile.Name,
            Port = profile.Port,
            CredentialProfileIds = profile.CredentialProfileIds,
        };
    }
}

