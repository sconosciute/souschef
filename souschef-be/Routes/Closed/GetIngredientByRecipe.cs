using FastEndpoints;
using souschef_be.Services;
using souschef_core.Model;
using souschef_core.Model.DTO;

namespace souschef_be.Routes.Closed;

public class GetIngredientByRecipe(RecipeService rSvc) : Endpoint<StupidDto, List<IngrRecipe>>
{
    public override void Configure()
    {
        Get("/recipe/ingredients/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(StupidDto req, CancellationToken ct)
    {
        
        var ingredients = await rSvc.GetRecipeIngredient(req.id);
        if (ingredients is not null)
        {
            Console.Out.WriteLine(ingredients);
            await SendAsync(ingredients, cancellation: ct);
        }
        else
        {
            await SendErrorsAsync(500, ct);
        }
    }
}