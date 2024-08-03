using souschef_core.Model;

namespace souschef_core.Services;

public interface IMessageSvc
{
    Message? GetMessage(int id);
    List<Message> GetAllMessages();
    Message SendMessage(Message msg);
    bool DeleteMessage(int id);
}