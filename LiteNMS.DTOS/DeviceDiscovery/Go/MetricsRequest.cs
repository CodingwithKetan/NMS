namespace LiteNMS.DTOS;

public class MetricsRequest
{
    public List<string> Ipranges { get; set; } = new List<string>();
    public List<CredentialDto> Credentials { get; set; } = new List<CredentialDto>();
    public int Port { get; set; } = 22;
}