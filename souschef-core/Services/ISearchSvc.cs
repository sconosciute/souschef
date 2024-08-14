using souschef_core.Model.DTO;

namespace souschef_core.Services;

public interface ISearchSvc
{
    public Task<List<ThinRecipe>> SearchByName(string name, int page, int pageSize);

    public Task<List<ThinRecipe>> SearchByTags(List<long> tagIds, int page, int pageSize);
}