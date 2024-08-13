using souschef_core.Model;
using souschef_core.Model.DTO;

namespace souschef_core.Services;

public interface IJwtUtil
{
    public string GenerateToken(User user, CancellationToken ct);
    public int? ValidateToken(string token, string username);
}