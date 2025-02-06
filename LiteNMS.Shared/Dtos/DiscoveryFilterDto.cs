namespace LiteNMS.Shared.Dtos;

public class DiscoveryFilterDto
{
    public string? Status { get; set; }
    public string? Subnet { get; set; }
    public int? ProfileId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}