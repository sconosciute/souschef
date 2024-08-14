namespace souschef_fe.Services;

using souschef_core.Model;
using souschef_core.Services;
using souschef_core.Model.DTO;

public class ClientSearchService(HttpClient api) : ISearchSvc
{
    
    private const string Uri = "http://localhost:5293/recipe";
    public async Task<List<ThinRecipe>> SearchByName(string name, int page, int pageSize)
    {
        var url = $"{Uri}/name/?q={name}&page={page}&pageSize={pageSize}";
        return await api.GetFromJsonAsync<List<ThinRecipe>>(url) ?? new List<ThinRecipe>();
    }

    public async Task<List<ThinRecipe>> SearchByTags(List<long> tagIds, int page, int pageSize)
    {
        return null;
    }
}