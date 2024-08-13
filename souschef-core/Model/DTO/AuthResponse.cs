namespace souschef_core.Model.DTO;

/// <summary>
/// DTO for responding to user authentication requests
/// </summary>
public class AuthResponse(int id, string username, string token)
{
    public int Id { get; init; } = id;
    public string Username { get; init; } = username;
    public string Token { get; init; } = token;
}