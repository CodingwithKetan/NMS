namespace Entities.Models;

public class DeviceMetricResult : BaseEntity
{
    public string IpAddress { get; set; }
    public string CPUUsage { get; set; }
    public string MemoryUsage { get; set; }
    public string DiskUsage { get; set; }
    public DateTime CollectedAt { get; set; }
}