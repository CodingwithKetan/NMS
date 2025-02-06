namespace Repository.Contract.Models;

public class DiscoveryProfile
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subnet { get; set; }
    public string ScanType { get; set; } // ICMP, SNMP, etc.
    public int Timeout { get; set; }
    public int Retries { get; set; }
}
