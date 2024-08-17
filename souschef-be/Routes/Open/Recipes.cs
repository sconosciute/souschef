using FastEndpoints;
using souschef_be.Services;
using souschef_core.Model;
using souschef_core.Model.DTO;
using souschef_core.Services;

namespace souschef_be.Routes.Open;

public class AddRecipe(ICrudSvc<Recipe> recipeSvc) : Endpoint<Recipe>
{
    public override void Configure()
    {
        Post("/recipe");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Recipe req, CancellationToken ct)
    {
        await SendAsync(await recipeSvc.AddAsync(req), cancellation: ct);
    }
}

public class GetRecipe(ICrudSvc<Recipe> recipeSvc) : Endpoint<Recipe, HumanReadableRecipe>
{
    public override void Configure()
    {
        Get("/recipe/{@recipe_id}", recipe => new { recipe.RecipeId });
        AllowAnonymous();
    }
    
    

    public override async Task HandleAsync(Recipe req, CancellationToken ct)
    {
        await SendAsync(await (await recipeSvc.GetAsync(req.RecipeId))?.ToHumanReadable()!, cancellation: ct);
    }
}

public class GetAllRecipes(ICrudSvc<Recipe> recipeSvc) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/recipe/all");
        AllowAnonymous();
    }

    // ask about this
    public override async Task HandleAsync(CancellationToken ct)
    {
        var recipes = await recipeSvc.GetAllAsync();
        Console.Out.WriteLine("converting to HRRecipe");
        List<HumanReadableRecipe> better = [];
        if (recipes != null)
            foreach (var r in recipes)
            {
                better.Add(await r.ToHumanReadable());
            }

        await SendAsync(better, cancellation: ct);
    }
    
    
}
public class UpdateRecipe(ICrudSvc<Recipe> recipeSvc) : Endpoint<Recipe>
{
    public override void Configure()
    {
        Put("/recipe");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Recipe req, CancellationToken ct)
    {
        await SendAsync(await recipeSvc.UpdateAsync(req, req.RecipeId), cancellation: ct);
    }
}
public class DeleteRecipe(ICrudSvc<Recipe> recipeSvc) : Endpoint<Recipe>
{
    public override void Configure()
    {
        Delete("/recipe/{@recipe_id}", recipe => new {recipe.RecipeId});
        AllowAnonymous();
    }

    public override async Task HandleAsync(Recipe req, CancellationToken ct)
    {
        if ( await recipeSvc.DeleteAsync(req.RecipeId) )
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            await SendErrorsAsync(500, ct);
        }
    }
}