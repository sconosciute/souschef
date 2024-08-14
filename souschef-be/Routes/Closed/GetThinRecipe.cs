using FastEndpoints;
using souschef_be.Services;
using souschef_core.Model;
using souschef_core.Model.DTO;

namespace souschef_be.Routes.Closed;

public class GetThinRecipe(ThinRecipeService recipeSvc) : Endpoint<ThinRecipe>
{
    public override void Configure()
    {
        Get("/thinrecipe");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ThinRecipe req, CancellationToken ct)
    {
        var recipes = await recipeSvc.GetRecipeInfoBasic(req.id);
    }
}