using souschef_core.Model;

namespace souschef_core.Services;

public interface IJwtService
{
    public string GenerateToken(User user, CancellationToken ct);
}