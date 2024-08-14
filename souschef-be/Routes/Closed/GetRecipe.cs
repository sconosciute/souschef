using FastEndpoints;
using souschef_be.Services;
using souschef_core.Model;
using souschef_core.Model.DTO;

namespace souschef_be.Routes.Closed;

public class GetRecipe(SkinnyRecipeService recipeSvc) : Endpoint<SkinnyRecipe>
{
    public override void Configure()
    {
        Get("/recipe");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SkinnyRecipe req, CancellationToken ct)
    {
        var recipes = await recipeSvc.GetRecipeInfoBasic(req.id);
    }
}