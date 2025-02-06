namespace Repository.Contract.Models;

public class Discovery
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public string Status { get; set; } // Running, Completed, Failed
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
