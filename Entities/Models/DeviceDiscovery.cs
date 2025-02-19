namespace Entities.Models;

public class DeviceDiscoveryResult : BaseEntity
{
    public Guid DeviceId { get; set; }
    public string IPAddress { get; set; }
    public string Status { get; set; } 
    public Guid CredentialProfileId { get; set; }
    public Guid DiscoveryProfileId { get; set; }
    public DateTime LastChecked { get; set; }
}
