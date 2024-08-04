using souschef_core.Model;

namespace souschef_core.Services;

public interface IMessageSvc
{
    Task<Message?> GetMessageAsync(int id);
    Task<List<Message>> GetAllMessagesAsync();
    Task<Message?> SendMessageAsync(Message? msg);
    Task<bool> DeleteMessageAsync(int id);
}