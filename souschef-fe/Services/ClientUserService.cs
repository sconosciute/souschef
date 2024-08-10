using System.Net;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_fe.Services;

public class ClientUserService(HttpClient api) : ICrudSvc<User>
{
    private const string Uri = "http://localhost:5293/user";
    
    // public async Task<User?> GetUserAsync(string username)
    // {
    //     return await api.GetFromJsonAsync<User>($"{Uri}/{username}");
    // }
    
    public async Task<User?> GetAsync(long id)
    {
        return await api.GetFromJsonAsync<User>($"{Uri}/{id}");
    }

    public async Task<List<User>?> GetAllAsync()
    {
        return await api.GetFromJsonAsync<List<User>>($"{Uri}/all") ?? [];
    }

    public async Task<User?> AddAsync(User? ent)
    {
        var res = await api.PostAsJsonAsync(Uri, ent);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<User>();
    }

    public async Task<User?> UpdateAsync(User? ent)
    {
        throw new NotImplementedException();
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