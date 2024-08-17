using Microsoft.EntityFrameworkCore;
using souschef_core.Model;
using souschef_core.Model.DTO;
using souschef_core.Services;

namespace souschef_be.Services;

public class PgRecipeSearchSvc(DbContext db) : ISearchSvc
{
    public async Task<List<ThinRecipe>> SearchByName(string name, int page, int pageSize)
    {
        return await db.Set<Recipe>()
            .Where(r => r.Name != null && r.Name.ToLower().Contains(name.ToLower()))
            .Select(r => new ThinRecipe
                { id = r.RecipeId, name = r.Name, description = r.Description, tags = r.GetTagEntities().Result })
            .OrderBy(r => r.id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<ThinRecipe>> SearchByTags(List<long> tagIds, int page, int pageSize)
    {
        return await db.Set<Recipe>()
            .Where(r => r.Tags != null && tagIds.All(t => r.Tags.Contains(t)))
            .Select(r => new ThinRecipe
            {
                id = r.RecipeId, description = r.Description, name = r.Name, tags = r.GetTagEntities().Result
            })
            .OrderBy(r => r.id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}