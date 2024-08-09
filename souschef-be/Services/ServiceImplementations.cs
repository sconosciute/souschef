using Microsoft.EntityFrameworkCore;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_be.Services;

public class MsgSvcComponent(ILogger<PgCrudSvcComponent<Message>> logger, DbContext db)
    : PgCrudSvcComponent<Message>(logger, db), IMessageSvc
{
    public Task<Message?> GetMessageAsync(long id) => GetAsync(id);
    public Task<List<Message>?> GetAllMessagesAsync() => GetAllAsync();
    public Task<Message?> AddMessageAsync(Message? msg) => AddAsync(msg);
    public Task<bool> DeleteMessageAsync(long id) => DeleteAsync(id);
}