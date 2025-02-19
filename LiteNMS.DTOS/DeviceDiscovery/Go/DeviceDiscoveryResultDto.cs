namespace LiteNMS.DTOS;

public class DeviceDiscoveryResultDto
{
    public string IPAddress { get; set; }
    public string Status { get; set; }
    public Guid CredentialProfileId { get; set; }
}