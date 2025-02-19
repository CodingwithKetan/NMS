using System.ComponentModel.DataAnnotations;
namespace LiteNMS.DTOS;

public class CredentialProfileRequest
{
    [Required]
    [MaxLength(100)]
    public string ProfileName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; }  // This will be encrypted before storing
}