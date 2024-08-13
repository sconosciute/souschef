using FastEndpoints;
using souschef_be.models;
using souschef_be.Services;
using souschef_core.Model.DTO;
using souschef_core.Services;

namespace souschef_be.Routes.Auth;

public class Login(UserService svc, IJwtService jwt) : Endpoint<AuthRequest, AuthResponse>
{
    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AuthRequest req, CancellationToken ct)
    {
        var user = await svc.QueryByUsername(req.Username);
        if (user is null)
        {
            await SendNotFoundAsync(cancellation: ct);
            return;
        }

        await SendAsync(new AuthResponse(user.UserId, user.Username!, jwt.GenerateToken(user, ct)));
    }
}