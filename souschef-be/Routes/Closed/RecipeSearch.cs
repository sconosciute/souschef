using FastEndpoints;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_be.Routes.Closed;

public class SearchRecipeName(ISearchSvc svc) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/recipe/name/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var kw = Query<string>(QueryParams.Query);
        var page = Query<int>(QueryParams.Page);
        var pSize = Query<int>(QueryParams.PageSize);

        await SendAsync(await svc.SearchByName(kw!, page, pSize), cancellation: ct);

    }
}

public class SearchRecipeTags(ISearchSvc svc) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/recipe/tag/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var tags = Query<List<long>>(QueryParams.Tag);
        var page = Query<int>(QueryParams.Page);
        var pSize = Query<int>(QueryParams.PageSize);
        await SendAsync(await svc.SearchByTags(tags, page, pSize), cancellation: ct);
    }
}