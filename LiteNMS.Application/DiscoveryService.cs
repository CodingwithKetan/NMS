using Entities.Models;
using LIteNMS.Application.Contracts;
using LiteNMS.DTOS;
using LiteNMS.DTOS.DeviceDiscovery.UI;
using LiteNMS.Infrastructure;

namespace LiteNMS.Services;

public class DiscoveryService : IDiscoveryService
{
    private readonly IDeviceDiscoveryRepository _repository;
    private readonly IDiscoveryProfileRepository _profileRepository;
    private readonly IGoPluginService _goPluginService;
    
    public DiscoveryService(IDeviceDiscoveryRepository repository, IGoPluginService goPluginService, IDiscoveryProfileRepository profileRepository)
    {
        _repository = repository;
        _profileRepository = profileRepository;
        _goPluginService = goPluginService;
    }
    
    public async Task<IEnumerable<DiscoveryResultUIDto>> RunDiscoveryAsync(Guid profileId)
    {
        var profile = await _profileRepository.GetByIdAsync(profileId);
        if (profile == null)
        {
            throw new ArgumentException("Invalid profile ID: Profile does not exist.");
        }
        
        var results = await _goPluginService.RunDiscovery(profile);
        var discoveries = results.Select(ToDeviceDiscoveryResult).ToList();
        
        await _repository.AddRangeAsync(discoveries);
        return discoveries.Select(ToDiscoveryResultUIDto).ToList();
    }
 
    private static DiscoveryResultUIDto ToDiscoveryResultUIDto(DeviceDiscoveryResult deviceDiscoveryResult)
    {
        return new DiscoveryResultUIDto()
        {
            Id = deviceDiscoveryResult.Id,
            IpAddress =  deviceDiscoveryResult.IPAddress,
            Status = deviceDiscoveryResult.Status,
            LastCheckedTime =  deviceDiscoveryResult.LastChecked.ToLongTimeString(),
        };
    }

    private static DeviceDiscoveryResult ToDeviceDiscoveryResult(DeviceDiscoveryResultDto deviceDiscoveryResultDto)
    {
        return new DeviceDiscoveryResult()
        {
            IPAddress = deviceDiscoveryResultDto.IPAddress,
            Status = deviceDiscoveryResultDto.Status,
            CredentialProfileId =  deviceDiscoveryResultDto.CredentialProfileId,
            LastChecked = DateTime.Now
        };
    }
}