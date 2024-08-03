using souschef_core.Model;
using souschef_core.Services;

namespace souschef_fe.Services;

public class ClientMessageService(HttpClient api) : IMessageSvc
{
    private string _uri = "/msg";
    public Message? GetMessage(int id)
    {
        var res = api.GetFromJsonAsync<Message>(_uri + $"/{id}");
        return res.Result;
    }

    public List<Message> GetAllMessages()
    {
        var res = api.GetFromJsonAsync<List<Message>>(_uri + $"/all");
        return res.Result ?? [];
    }

    public Message SendMessage(Message msg)
    {
        throw new NotImplementedException();
    }

    public bool DeleteMessage(int id)
    {
        throw new NotImplementedException();
    }
}