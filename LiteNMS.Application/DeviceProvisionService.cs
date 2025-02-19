using Entities.Models;
using LIteNMS.Application.Contracts;
using LiteNMS.DTOS;
using LiteNMS.Infrastructure;
using LiteNMS.Infrastructure.Cache;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LiteNMS.Services;

public class DeviceProvisionService : IDeviceProvisionService
{
    private readonly IDeviceProvisionRepository _deviceProvisionRepository;
    private readonly IDeviceDiscoveryRepository _deviceDiscoveryRepository;
    private readonly IDeviceProvisionCacheRepository _deviceProvisionCacheRepository;

    public DeviceProvisionService(IDeviceProvisionRepository deviceProvisionRepository,
        IDeviceDiscoveryRepository deviceDiscoveryRepository,
        IDeviceProvisionCacheRepository deviceProvisionCacheRepository)
    {
        _deviceProvisionRepository = deviceProvisionRepository;
        _deviceDiscoveryRepository = deviceDiscoveryRepository;
        _deviceProvisionCacheRepository = deviceProvisionCacheRepository;
    }

    public async Task<Guid> ProvisionDevices(DeviceProvisionRequestDto request)
    {
        var discoveredDevices =  await _deviceDiscoveryRepository.GetDiscoveredDevices(request.DiscoveryProfileId);
        
        var deviceMerticProvision = ToDeviceMetricProvision(discoveredDevices);
        deviceMerticProvision.PollTime = request.PollTime;
        await _deviceProvisionRepository.AddAsync(deviceMerticProvision);
        _deviceProvisionCacheRepository.AddOrUpdate(deviceMerticProvision);
        return deviceMerticProvision.Id;
    }


    private static DeviceMetricProvision ToDeviceMetricProvision(IEnumerable<DeviceDiscoveryResult> discoveryResults)
    {
        var deviceMetricProvision = new DeviceMetricProvision();
        deviceMetricProvision.Id = Guid.NewGuid();
        foreach (var discoveryResult in discoveryResults)
        {
            deviceMetricProvision.IpAddresses.Add(discoveryResult.IPAddress);
            deviceMetricProvision.CredentialProfileIds.Add(discoveryResult.CredentialProfileId);
        }
        return deviceMetricProvision;
    }
}