using System.Net;
using System.Text.RegularExpressions;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_fe.Services;

public class ClientTagService(HttpClient api) : ICrudSvc<Tag>
{
    private const string Uri = "http://localhost:5293/tag";
    
    
    
    public async Task<Tag?> GetAsync(long id)
    {
        return await api.GetFromJsonAsync<Tag>($"{Uri}/{id}");
    }

    public async Task<List<Tag>?> GetAllAsync()
    {
        return await api.GetFromJsonAsync<List<Tag>>($"{Uri}/all") ?? [];
    }

    public async Task<Tag?> AddAsync(Tag? tag)
    {
        // user.Photo.Initialize();
        var res = await api.PostAsJsonAsync(Uri, tag);
        // var res = await api.PostAsync(user);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<Tag>();
    }

    public async Task<Tag?> UpdateAsync(Tag? tag, long id)
    {
        // var res = await api.PutAsJsonAsync(Uri, ent);
        var res = await api.PutAsJsonAsync($"{Uri}/{id}", tag);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<Tag>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var res = await api.DeleteAsync($"{Uri}/{id}");
        return res.StatusCode == HttpStatusCode.NoContent;
    }
    
    
    
    
    
    
    
    
    
    
    

    public async Task<User?> SendUserAsync(User? user)
    {
        var res = await api.PostAsJsonAsync(Uri, user);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<User>();
    }

    public async Task<bool> DeleteUserAsync(long id)
    {
        var res = await api.DeleteAsync($"{Uri}/{id}");
        return res.StatusCode == HttpStatusCode.NoContent;
    }
    
}