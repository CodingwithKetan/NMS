namespace Entities.Models;

public class DiscoveryProfile : BaseEntity
{
    public string Name { get; set; }
    public List<string> IPRanges { get; set; }
    public List<Guid> CredentialProfileIds { get; set; }
    public int Port { get; set; } = 22;
}