using Microsoft.EntityFrameworkCore;
using souschef_core.Model;

namespace souschef_be.Services;

public class RecipeService(ILogger<PgCrudSvcComponent<Recipe>> logger, DbContext db) : PgCrudSvcComponent<Recipe>(logger, db)
{
    private readonly DbContext _db = db;

    public async Task<List<IngrRecipe>?> GetRecipeIngredient(long recipeId)
    {
        
        
        return await _db.Set<IngrRecipe>()
            .Where(i => i.RecipeId == recipeId)
            .ToListAsync();
    }
    
    public async Task<List<bullshit>?> GetRecipeIngredientToo(long recipeId)
    {
        var recipes = _db.Set<Recipe>();
        var ingrRec = _db.Set<IngrRecipe>();
        var ingredient = _db.Set<Ingredient>();

        var q = from recipe in recipes
            join ingrForRec in ingrRec on recipe.RecipeId equals ingrForRec.RecipeId
            join ingr in ingredient on ingrForRec.IngrId equals ingr.IngrId
            select new bullshit{ Name = recipe.Name, thang = ingrForRec, otherThang = ingr };
        
        return await q.ToListAsync();
    }

    public record bullshit
    {
        public string Name { get; set; }
        public IngrRecipe thang { get; set; }
        public Ingredient otherThang { get; set; }
        
    }
}