using Microsoft.EntityFrameworkCore;
using souschef_core.Model;
using souschef_core.Model.DTO;

namespace souschef_be.Services;

public class SkinnyRecipeService(ILogger<PgCrudSvcComponent<Recipe>> logger, DbContext db) : PgCrudSvcComponent<Recipe>(logger, db)
{
    private readonly DbContext _db = db;
    
    public async Task<SkinnyRecipe?> GetRecipeInfoBasic(long recipeId)
    {
        var recipe = _db.Set<Recipe>()
            .Single(i => i.RecipeId == recipeId);

        var output = new SkinnyRecipe{name = recipe.Name, description = recipe.Description, tags = recipe.Tags};
        
        return output;


    }
}