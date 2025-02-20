namespace NMS.WebAPI.Dtos;

public class UpdateDiscoveryProfileDto
{
    public string Name { get; set; }
    public string Subnet { get; set; }
    public string ScanType { get; set; }
    public int Timeout { get; set; }
    public int Retries { get; set; }
}