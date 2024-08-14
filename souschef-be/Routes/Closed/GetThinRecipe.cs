using FastEndpoints;
using souschef_be.Services;
using souschef_core.Model;
using souschef_core.Model.DTO;

namespace souschef_be.Routes.Closed;

public class GetThinRecipe(ThinRecipeService recipeSvc) : Endpoint<ThinRecipe>
{
    public override void Configure()
    {
        Get("/thinrecipe/{@recipe_id}", recId => new {recId.id});
        AllowAnonymous();
    }

    public override async Task HandleAsync(ThinRecipe req, CancellationToken ct)
    {
        await SendAsync(await recipeSvc.GetRecipeInfoBasic(req.id), cancellation: ct);
    }
}