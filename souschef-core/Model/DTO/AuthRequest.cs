using System.ComponentModel.DataAnnotations;

namespace souschef_core.Model.DTO;

/// <summary>
/// DTO for user authentication requests
/// </summary>
public class AuthRequest(string user, string pass)
{
    [Required]
    public string Username { get; init;  } = user;

    [Required]
    public string Password { get; init; } = pass;
}