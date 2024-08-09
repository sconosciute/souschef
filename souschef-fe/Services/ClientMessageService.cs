using System.Net;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_fe.Services;

public class ClientMessageService(HttpClient api) : ICrudSvc<Message>
{
    private const string Uri = "http://localhost:5293/msg";

    public async Task<Message?> GetAsync(long id)
    {
        return await api.GetFromJsonAsync<Message>($"{Uri}/{id}");
    }

    public async Task<List<Message>?> GetAllAsync()
    {
        return await api.GetFromJsonAsync<List<Message>>($"{Uri}/all") ?? [];
    }

    public async Task<Message?> AddAsync(Message? ent)
    {
        var res = await api.PostAsJsonAsync(Uri, ent);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<Message>();
    }

    public async Task<Message?> UpdateAsync(Message? ent)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var res = await api.DeleteAsync($"{Uri}/{id}");
        return res.StatusCode == HttpStatusCode.NoContent;
    }
}