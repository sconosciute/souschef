using System.Net;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_fe.Services;

public class ClientUserService(HttpClient api) : IUserSvc
{
    private const string Uri = "http://localhost:5293/user";
    
    public async Task<User?> GetUserAsync(string username)
    {
        return await api.GetFromJsonAsync<User>($"{Uri}/{username}");
    }
    
    public async Task<User?> SendUserAsync(User? user)
    {
        var res = await api.PostAsJsonAsync(Uri, user);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<User>();
    }
    
}