using Microsoft.EntityFrameworkCore;
using souschef_be.models;
using souschef_core.Exceptions;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_be.Services;

internal class PgDbService : IMessageSvc
{
    private readonly SouschefContext _db;
    private readonly ILogger _log;
    private readonly IMessageSvc _msgService;

    public PgDbService(ILogger<PgDbService> logger, ILoggerFactory logFactory, SouschefContext db)
    {
        _db = db;
        _log = logger;
        _msgService = new MsgSvcComponent(logFactory.CreateLogger<PgCrudSvcComponent<Message>>(), _db);
    }

    public List<Measurement> GetAllMeasures()
    {
        try
        {
            return _db.Measurements.ToList();
        }
        catch (ArgumentNullException e)
        {
            return [];
        }
    }

    public async Task<Message?> GetMessageAsync(long id) => await _msgService.GetMessageAsync(id);

    public async Task<List<Message>?> GetAllMessagesAsync() => await _msgService.GetAllMessagesAsync();

    public async Task<Message?> AddMessageAsync(Message? msg) => await _msgService.AddMessageAsync(msg);

    public async Task<bool> DeleteMessageAsync(long id) => await _msgService.DeleteMessageAsync(id);
}