namespace souschef_core.Model.DTO;

/// <summary>
/// DTO for responding to user authentication requests
/// </summary>
public class AuthResponse(long id, string username, string displayName, string token)
{
    public long Id { get; init; } = id;
    public string Username { get; init; } = username;
    public string DisplayName { get; init; }
    public string Token { get; init; } = token;

    public override string ToString()
    {
        return $"AuthResponse {{Id: {Id}, Username: {Username}, DisplayName: {DisplayName}, Token: {Token} }}";
    }
}