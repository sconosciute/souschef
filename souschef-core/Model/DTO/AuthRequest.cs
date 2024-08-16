using System.ComponentModel.DataAnnotations;

namespace souschef_core.Model.DTO;

/// <summary>
/// DTO for user authentication requests
/// </summary>
public class AuthRequest(string username, string password)
{
    public AuthRequest() : this(string.Empty, string.Empty)
    {
    }

    [Required]
    public string Username { get; set;  } = username;

    [Required]
    public string Password { get; set; } = password;
}