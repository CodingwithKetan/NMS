namespace LiteNMS.DTOS;

public record CredentialDto  
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}