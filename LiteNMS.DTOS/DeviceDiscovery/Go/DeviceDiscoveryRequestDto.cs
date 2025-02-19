namespace LiteNMS.DTOS;

public class DeviceDiscoveryRequestDto
{
    public string Type { get; } = "discovery";
    public DiscoveryRequest Discovery { get; set; } = new DiscoveryRequest(); // Ensure it's initialized
    public MetricsRequest Metrics { get; set; } = new MetricsRequest(); // Ensure it's initialized
}