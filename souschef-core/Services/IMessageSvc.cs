using souschef_core.Model;

namespace souschef_core.Services;

public interface IMessageSvc
{
    Task<Message?> GetMessageAsync(long id);
    Task<List<Message>?> GetAllMessagesAsync();
    Task<Message?> SendMessageAsync(Message? msg);
    Task<bool> DeleteMessageAsync(long id);
}