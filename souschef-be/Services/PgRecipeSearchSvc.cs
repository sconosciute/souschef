using souschef_core.Model.DTO;
using souschef_core.Services;

namespace souschef_be.Services;

public class PgRecipeSearchSvc : ISearchSvc
{
    public async Task<ThinRecipe?> SearchByName(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<ThinRecipe?> SearchByTags(List<long> tagIds)
    {
        throw new NotImplementedException();
    }
}