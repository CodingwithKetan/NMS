namespace LiteNMS.DTOS;

public class DiscoveryRequest
{
    public List<string> Ipranges { get; set; } = new List<string>(); // Default empty list
    public List<CredentialDto> Credentials { get; set; } = new List<CredentialDto>(); // Default empty list
    public int Port { get; set; } = 22;
}