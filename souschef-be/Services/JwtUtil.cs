using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using souschef_core.Model;
using souschef_core.Model.DTO;
using souschef_core.Services;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;

namespace souschef_be.Services;

public class JwtUtil(UserService userSvc, IConfiguration config) : IJwtUtil
{
    /// <summary>
    /// Generates a new JSON Web Token for the given user. Tokens are valid for 30 days.
    /// </summary>
    /// <param name="user">The user to provide a token for.</param>
    /// <param name="ct">Cancellation token to cancel this request.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>A JSON Web Token for the given user.</returns>
    public string GenerateToken(User user, CancellationToken ct)
    {
        var tokinator = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") ??
                                                                   throw new InvalidOperationException()));
        var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub,
                user.UserId.ToString() ?? throw new ArgumentNullException(nameof(user.UserId))),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.Username ?? throw new ArgumentNullException(nameof(user.Username)))
        };

        var desc = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(config["JWT:ExpiryDays"])),
            IssuedAt = DateTime.UtcNow,
            Issuer = config["JWT:Issuer"],
            Audience = config["JWT:Audience"],
            SigningCredentials = signature
        };

        var token = tokinator.CreateToken(desc);
        return tokinator.WriteToken(token);
    }

    /// <summary>
    /// Validates a given JSON Web Token.
    /// </summary>
    /// <param name="token">JWT to validate.</param>
    /// <param name="username"></param>
    /// <returns>User ID from token if valid or null if token is invalid.</returns>
    public async Task<int?> ValidateToken(string token, string username)
    {
        throw new NotImplementedException();
        return null;
    }
}