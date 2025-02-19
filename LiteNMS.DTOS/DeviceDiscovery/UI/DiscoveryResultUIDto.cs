namespace LiteNMS.DTOS.DeviceDiscovery.UI;

public class DiscoveryResultUIDto
{
    public Guid Id { get; set; }
    public string IpAddress { get; set; }
    public string Status { get; set; }
    public string LastCheckedTime { get; set; }
}