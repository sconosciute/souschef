using Microsoft.EntityFrameworkCore;
using souschef_core.Model;
using souschef_core.Model.DTO;

namespace souschef_be.Services;

public class SkinnyRecipeService(ILogger<PgCrudSvcComponent<Recipe>> logger, DbContext db) : PgCrudSvcComponent<Recipe>(logger, db)
{
    private readonly DbContext _db = db;

    // i think this is just getting a recipe id and returning recipe id's
    public async Task<List<Recipe>?> GetRecipeName(long recipeId)
    {
        return await _db.Set<Recipe>()
            .Where(i => i.RecipeId == recipeId)
            .ToListAsync();
    }

    public async Task<List<Recipe>?> GetRecipeInfoBasic(long recipeID)
    {
        var recipes = _db.Set<Recipe>();
        var output = from recipe in recipes
            select new SkinnyRecipe{name = recipe.Name, description = recipe.Description, tags = recipe.Tags};
        // ask about this. We need to output each object as a list
        return await output.ToListAsync();


    }
}