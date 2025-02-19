namespace LiteNMS.DTOS;

public class DiscoveryProfileRequestDto
{
    public string Name { get; set; }
    public List<string> IpRanges { get; set; }
    public List<Guid> CredentialProfileIds { get; set; }
    public int Port { get; set; } = 22;
}