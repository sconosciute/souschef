using souschef_core.Model.DTO;

namespace souschef_core.Services;

public interface ISearchSvc
{
    public Task<List<ThinRecipe>> SearchByName(string name);

    public Task<List<ThinRecipe>> SearchByTags(List<long> tagIds);
}