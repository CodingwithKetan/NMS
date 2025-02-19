namespace LiteNMS.DTOS;

public class DeviceProvisionRequestDto
{
    public Guid DiscoveryProfileId { get; set; }
    public int PollTime { get; set; }
}
