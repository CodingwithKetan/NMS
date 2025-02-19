namespace Entities.Models;

public class DeviceMetricProvision : BaseEntity
{
    public List<string> IpAddresses { get; set; } = new List<string>();
    public List<Guid> CredentialProfileIds { get; set; } = new List<Guid>();
    public int PollTime { get; set; }
}