using Microsoft.EntityFrameworkCore;
using souschef_core.Model;
using souschef_core.Model.DTO;

namespace souschef_be.Services;

public class ThinRecipeService(ILogger<PgCrudSvcComponent<Recipe>> logger, DbContext db) : PgCrudSvcComponent<Recipe>(logger, db)
{
    private readonly DbContext _db = db;
    
    public async Task<ThinRecipe?> GetRecipeInfoBasic(long recipeId)
    {
        var recipe = _db.Set<Recipe>()
            .Single(i => i.RecipeId == recipeId);
        
        var output = new ThinRecipe{name = recipe.Name, description = recipe.Description, tags = recipe.Tags};
        
        return output;


    }
}