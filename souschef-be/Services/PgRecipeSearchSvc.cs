using Microsoft.EntityFrameworkCore;
using souschef_core.Model;
using souschef_core.Model.DTO;
using souschef_core.Services;

namespace souschef_be.Services;

public class PgRecipeSearchSvc(DbContext db) : ISearchSvc
{
    public async Task<List<ThinRecipe>> SearchByName(string name)
    {
        return await db.Set<Recipe>()
            .Where(r => r.Name != null && r.Name.Contains(name))
            .Select(r => new ThinRecipe
                { id = r.RecipeId, name = r.Name, description = r.Description, tags = r.GetTagEntities().Result })
            .ToListAsync();
    }

    public async Task<List<ThinRecipe>> SearchByTags(List<long> tagIds)
    {
        return await db.Set<Recipe>()
            .Where(r => r.Tags != null && r.Tags.Any(tagIds.Contains))
            .Select(r => new ThinRecipe
            {
                id = r.RecipeId, description = r.Description, name = r.Name, tags = r.GetTagEntities().Result
            })
            .ToListAsync();
    }
}