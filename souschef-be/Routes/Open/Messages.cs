using FastEndpoints;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_be.Routes.Open;

public class CreateMessage(ICrudSvc<Message> messageSvc) : Endpoint<Message>
{
    public override void Configure()
    {
        Post("/msg");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Message req, CancellationToken ct)
    {
        await SendAsync(await messageSvc.AddAsync(req), cancellation: ct);
    }
}

public class GetMessage(ICrudSvc<Message> messageSvc) : Endpoint<Message>
{
    public override void Configure()
    {
        Get("/msg/{@msgId}", msg => new { msg.MsgId });
        AllowAnonymous();
    }

    public override async Task HandleAsync(Message req, CancellationToken ct)
    {
        await SendAsync(await messageSvc.GetAsync(req.MsgId), cancellation: ct);
    }
}

public class GetAllMessages(ICrudSvc<Message> messageSvc) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/msg");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(await messageSvc.GetAllAsync(), cancellation: ct);
    }
}

public class UpdateMessage(ICrudSvc<Message> messageSvc) : Endpoint<Message>
{
    public override void Configure()
    {
        Put("/msg");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Message req, CancellationToken ct)
    {
        await SendAsync(await messageSvc.UpdateAsync(req, req.MsgId), cancellation: ct);
    }
}

public class DeleteMessage(ICrudSvc<Message> messageSvc) : Endpoint<Message>
{
    public override void Configure()
    {
        Delete("/msg/{@msgId}", msg => new {msg.MsgId});
        AllowAnonymous();
    }

    public override async Task HandleAsync(Message req, CancellationToken ct)
    {
        if ( await messageSvc.DeleteAsync(req.MsgId) )
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            await SendErrorsAsync(500, ct);
        }
    }
}