using System.Net;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_fe.Services;

public class ClientMessageService(HttpClient api) : IMessageSvc
{
    private const string Uri = "/msg";

    public async Task<Message?> GetMessageAsync(int id)
    {
        return await api.GetFromJsonAsync<Message>($"{Uri}/{id}");
    }

    public async Task<List<Message>> GetAllMessagesAsync()
    {
        return await api.GetFromJsonAsync<List<Message>>($"{Uri}/all") ?? [];
    }

    public async Task<Message?> SendMessageAsync(Message? msg)
    {
        var res = await api.PostAsJsonAsync(Uri, msg);
        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<Message>();
    }

    public async Task<bool> DeleteMessageAsync(int id)
    {
        var res = await api.DeleteAsync($"{Uri}/{id}");
        return res.StatusCode == HttpStatusCode.NoContent;
    }
}