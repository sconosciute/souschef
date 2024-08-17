using System.Net;
using System.Text.RegularExpressions;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_fe.Services;

public class ClientUserService(HttpClient api) : ICrudSvc<User>
{
    private const string Uri = "http://localhost:5293/user";
    
    
    
    public async Task<User?> GetAsync(long id)
    {
        return await api.GetFromJsonAsync<User>($"{Uri}/{id}");
    }

    public async Task<List<User>?> GetAllAsync()
    {
        return await api.GetFromJsonAsync<List<User>>($"{Uri}/all") ?? [];
    }

    public async Task<User?> AddAsync(User? user)
    {
        // user.Photo.Initialize();
        var res = await api.PostAsJsonAsync(Uri, user);
        // var res = await api.PostAsync(user);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<User>();
    }

    public async Task<User?> UpdateAsync(User? user, long id)
    {
        // var res = await api.PutAsJsonAsync(Uri, ent);
        var res = await api.PutAsJsonAsync($"{Uri}/{id}", user);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<User>();
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