using FastEndpoints;
using souschef_be.Services;
using souschef_core.Model;
using souschef_core.Model.DTO;
using souschef_core.Services;
using BC = BCrypt.Net.BCrypt;

namespace souschef_be.Routes.Auth;

public class Register(UserService userSvc, IJwtService jwt) : Endpoint<RegisterUserRequest, AuthResponse>
{
    public override void Configure()
    {
        Post("/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterUserRequest req, CancellationToken ct)
    {
        var user = new User
        {
            Photo = req.Photo,
            Username = req.Username,
            HashedPass = BC.HashPassword(req.Password),
            DisplayName = req.DisplayName ?? req.Username,
            Email = req.Email,
            Firstname = req.FirstName,
            Lastname = req.LastName
        };

        var newUser = await userSvc.AddAsync(user);
        if (newUser is null)
        {
            await SendErrorsAsync(500, ct);
            return;
        }
        var token = jwt.GenerateToken(newUser, ct);

        await SendAsync(new AuthResponse(newUser.UserId, newUser.Username!, token));
    }
}