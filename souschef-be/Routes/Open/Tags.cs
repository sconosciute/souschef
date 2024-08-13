namespace souschef_be.Routes.Open;

using FastEndpoints;
using souschef_core.Model;
using souschef_core.Services;

public class CreateTag(ICrudSvc<Tag> tagSvc) : Endpoint<Tag>
{
    public override void Configure()
    {
        Post("/tag");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Tag req, CancellationToken ct)
    {
        await SendAsync(await tagSvc.AddAsync(req), cancellation: ct);
    }
}

public class GetTag(ICrudSvc<Tag> tagSvc) : Endpoint<Tag>
{
    public override void Configure()
    {
        Get("/tag/{@tagId}", tag => new { tag.TagId });
        AllowAnonymous();
    }

    public override async Task HandleAsync(Tag req, CancellationToken ct)
    {
        await SendAsync(await tagSvc.GetAsync(req.TagId), cancellation: ct);
    }
}

public class GetAllTags(ICrudSvc<Tag> tagSvc) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/tag/all");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(await tagSvc.GetAllAsync(), cancellation: ct);
    }
}

public class UpdateTag(ICrudSvc<Tag> tagSvc) : Endpoint<Tag>
{
    public override void Configure()
    {
        // Put("/user");
        Put("/tag/{@tagId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Tag req, CancellationToken ct)
    {
        await SendAsync(await tagSvc.UpdateAsync(req, req.TagId), cancellation: ct);
    }
}

public class DeleteTag(ICrudSvc<Tag> tagSvc) : Endpoint<Tag>
{
    public override void Configure()
    {
        Delete("/tag/{@tagId}", tag => new {tag.TagId});
        AllowAnonymous();
    }

    public override async Task HandleAsync(Tag req, CancellationToken ct)
    {
        if ( await tagSvc.DeleteAsync(req.TagId) )
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            await SendErrorsAsync(500, ct);
        }
    }
}