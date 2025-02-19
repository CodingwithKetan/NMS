namespace Entities.Models;
using System.ComponentModel.DataAnnotations;

public class CredentialProfile : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string ProfileName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Username { get; set; }

    [Required]
    public string EncryptedPassword { get; set; } // Will store encrypted passwords
}