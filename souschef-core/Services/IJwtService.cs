using souschef_core.Model;
using souschef_core.Model.DTO;

namespace souschef_core.Services;

public interface IJwtService
{
    public string GenerateToken(User user, CancellationToken ct);
}