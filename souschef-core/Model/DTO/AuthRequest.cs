using System.ComponentModel.DataAnnotations;

namespace souschef_core.Model.DTO;

/// <summary>
/// DTO for user authentication requests
/// </summary>
public class AuthRequest(string username, string password)
{
    [Required]
    public string Username { get; init;  } = username;

    [Required]
    public string Password { get; init; } = password;
}