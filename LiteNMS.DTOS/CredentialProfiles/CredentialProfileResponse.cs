namespace LiteNMS.DTOS;

public class CredentialProfileResponse
{
    public Guid Id { get; set; }
    public string ProfileName { get; set; }
    public string Username { get; set; }
    public DateTime CreatedAt { get; set; }
}