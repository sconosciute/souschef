using System.IdentityModel.Tokens.Jwt;
using FastEndpoints.Security;
using souschef_core.Model;
using souschef_core.Model.DTO;
using souschef_core.Services;
using BCrypt.Net;

namespace souschef_be.Services;

public class JwtUtil(UserService userSvc) : IJwtUtil
{
    /// <summary>
    /// Generates a new JSON Web Token for the given user. Tokens are valid for 30 days.
    /// </summary>
    /// <param name="user">The user to provide a token for.</param>
    /// <param name="ct">Cancellation token to cancel this request.</param>
    /// <returns>A JSON Web Token for the given user.</returns>
    public string GenerateToken(User user, CancellationToken ct)
    {
        return JwtBearer.CreateToken(
            t =>
            {
                t.ExpireAt = DateTime.UtcNow.AddDays(30);
                t.User.Roles.Add("User");
                t.User.Claims.Add(("Username", user.Username!));
                t.User
            });
    }

    /// <summary>
    /// Validates a given JSON Web Token.
    /// </summary>
    /// <param name="token">JWT to validate.</param>
    /// <param name="username"></param>
    /// <returns>User ID from token if valid or null if token is invalid.</returns>
    public async Task<int?> ValidateToken(string token, string username)
    {
        var user = userSvc.QueryByUsername(username);
    }
    
}