using souschef_core.Model.DTO;

namespace souschef_core.Services;

public interface ISearchSvc
{
    public Task<ThinRecipe?> SearchByName(string name);

    public Task<ThinRecipe?> SearchByTags(List<long> tagIds);
}