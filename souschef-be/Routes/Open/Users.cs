using souschef_be.Services;

namespace souschef_be.Routes.Open;

using FastEndpoints;
using souschef_core.Model;
using souschef_core.Services;

public class CreateUser(UserService userSvc) : Endpoint<User>
{
    public override void Configure()
    {
        Post("/user");
        AllowAnonymous();
    }

    public override async Task HandleAsync(User req, CancellationToken ct)
    {
        await SendAsync(await userSvc.AddAsync(req), cancellation: ct);
    }
}

public class GetUser(UserService userSvc) : Endpoint<User>
{
    public override void Configure()
    {
        Get("/user/{@userId}", user => new { user.UserId });
        AllowAnonymous();
    }

    public override async Task HandleAsync(User req, CancellationToken ct)
    {
        await SendAsync(await userSvc.GetAsync(req.UserId), cancellation: ct);
    }
}

public class GetAllUsers(UserService userSvc) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/user");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(await userSvc.GetAllAsync(), cancellation: ct);
    }
}

public class UpdateUser(UserService userSvc) : Endpoint<User>
{
    public override void Configure()
    {
        // Put("/user");
        Put("/user/{@UserId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(User req, CancellationToken ct)
    {
        await SendAsync(await userSvc.UpdateAsync(req, req.UserId), cancellation: ct);
    }
}

public class DeleteUser(UserService userSvc) : Endpoint<User>
{
    public override void Configure()
    {
        Delete("/user/{@userId}", user => new {user.UserId});
        AllowAnonymous();
    }

    public override async Task HandleAsync(User req, CancellationToken ct)
    {
        if ( await userSvc.DeleteAsync(req.UserId) )
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            await SendErrorsAsync(500, ct);
        }
    }
}