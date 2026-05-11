using System.ComponentModel.DataAnnotations;
namespace SecureDotnetApiFoundation.Api.Dtos;
public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
