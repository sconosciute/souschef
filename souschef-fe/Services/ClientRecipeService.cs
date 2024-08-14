using souschef_core.Model.DTO;

namespace souschef_fe.Services;

using souschef_core.Model;
using souschef_core.Services;

using System.Net;

public class ClientRecipeService(HttpClient api) : ICrudSvc<Recipe>
{
    private const string Uri = "http://localhost:5293/recipe";
    
    
    
    public async Task<Recipe?> GetAsync(long id)
    {
        return await api.GetFromJsonAsync<Recipe>($"{Uri}/{id}");
    }
    
    public async Task<List<Recipe>?> GetAllAsync()
    {
        return await api.GetFromJsonAsync<List<Recipe>>($"{Uri}/all") ?? [];
    }

    public async Task<Recipe?> AddAsync(Recipe? rec)
    {
        // user.Photo.Initialize();
        var res = await api.PostAsJsonAsync(Uri, rec);
        // var res = await api.PostAsync(user);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<Recipe>();
    }

    public async Task<Recipe?> UpdateAsync(Recipe? ent, long id)
    {
        // var res = await api.PutAsJsonAsync(Uri, ent);
        var res = await api.PutAsJsonAsync($"{Uri}/{id}", ent);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<Recipe>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var res = await api.DeleteAsync($"{Uri}/{id}");
        return res.StatusCode == HttpStatusCode.NoContent;
    }
}