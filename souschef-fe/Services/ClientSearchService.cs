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

    // public async Task<List<ThinRecipe>> SearchByTags(List<long> tagIds, int page, int pageSize)
    // {
    //     // /recipe/tag?tags[]={tag}&page={resultsPage}&pageSize={ResultsPerPage}
    //     // return null;
    //
    //     var url = $"{Uri}/tag?tags[]={tagIds}&page={page}&pageSize={pageSize}";
    //     return await api.GetFromJsonAsync<List<ThinRecipe>>(url) ?? new List<ThinRecipe>();
    // }
    
    public async Task<List<ThinRecipe>> SearchByTags(List<long> tagIds, int page, int pageSize)
    {
        
        var tagQuery = string.Join("&", tagIds.Select(id => $"tag={id}"));
     
        var url = $"{Uri}/tag/?{tagQuery}&page={page}&pageSize={pageSize}";

        Console.WriteLine("URL: " + url);

        return await api.GetFromJsonAsync<List<ThinRecipe>>(url) ?? new List<ThinRecipe>();
    }




    
}