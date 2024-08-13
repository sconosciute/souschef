using FastEndpoints;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_be.Routes.Open;

public class AddIngredient(ICrudSvc<Ingredient> ingredientSvc) : Endpoint<Ingredient>
{
    public override void Configure()
    {
        Post("/ingr");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Ingredient req, CancellationToken ct)
    {
        await SendAsync(await ingredientSvc.AddAsync(req), cancellation: ct);
    }
}

public class GetIngredient(ICrudSvc<Ingredient> ingredientSvc) : Endpoint<Ingredient>
{
    public override void Configure()
    {
        Get("/ingr/{@ingr_id}", ingr => new { ingr.IngrId });
        AllowAnonymous();
    }

    public override async Task HandleAsync(Ingredient req, CancellationToken ct)
    {
        await SendAsync(await ingredientSvc.GetAsync(req.IngrId), cancellation: ct);
    }
}

public class GetAllIngredients(ICrudSvc<Ingredient> ingredientSvc) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/ingr");
        AllowAnonymous();
    }

    // ask about this
    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(await ingredientSvc.GetAllAsync(), cancellation: ct);
    }
}
public class UpdateIngredient(ICrudSvc<Ingredient> ingredientSvc) : Endpoint<Ingredient>
{
    public override void Configure()
    {
        Put("/ingr");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Ingredient req, CancellationToken ct)
    {
        await SendAsync(await ingredientSvc.UpdateAsync(req, req.IngrId), cancellation: ct);
    }
}
public class DeleteIngredient(ICrudSvc<Ingredient> ingredientSvc) : Endpoint<Ingredient>
{
    public override void Configure()
    {
        Delete("/ingr/{@ingr_Id}", ingr => new {ingr.IngrId});
        AllowAnonymous();
    }

    public override async Task HandleAsync(Ingredient req, CancellationToken ct)
    {
        if ( await ingredientSvc.DeleteAsync(req.IngrId) )
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            await SendErrorsAsync(500, ct);
        }
    }
}