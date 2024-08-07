using Microsoft.EntityFrameworkCore;
using souschef_be.models;
using souschef_core.Exceptions;
using souschef_core.Model;

namespace souschef_be.Services;

internal class PgDbService : IBeMessageSvc, IMeasurementSvc
{
    private readonly SouschefContext _db = new();
    private readonly ILogger _log;
    private readonly PgCrudSvcComp<Message> _msgComp; 

    public PgDbService(ILogger<PgDbService> logger, ILoggerFactory logFactory)
    {
        _log = logger;
        _msgComp = new PgCrudSvcComp<Message>(logFactory.CreateLogger<PgCrudSvcComp<Message>>(), _db);
    }
    
    
    // public async Task<Message?> GetMessageAsync(int id)
    // {
    //     return await _db.Messages.FindAsync(id);
    // }
    //
    // public async Task<List<Message>?> GetAllMessagesAsync()
    // {
    //     return await _db.Messages.ToListAsync();
    // }
    //
    // public async Task<Message?> SendMessageAsync(Message? msg)
    // {
    //     _log.LogDebug("Received message to post:{msg}", msg);
    //     var res = (await _db.Messages.AddAsync(msg)).Entity;
    //     var post = await _db.SaveChangesAsync();
    //     return post == 1 ? res : throw new DbApiFailureException("POST to DB resulted in 0 or multiple rows returned");
    // }
    //
    // public async Task<bool> DeleteMessageAsync(int id)
    // {
    //     var msg = await _db.Messages.FindAsync(id);
    //     if (msg is null)
    //     {
    //         _log.LogDebug("Service attempted to delete null message");
    //         return false;
    //     }
    //
    //     _db.Messages.Remove(msg);
    //     var deleted = await _db.SaveChangesAsync();
    //     return deleted == 1 ? true : throw new DbApiFailureException("DELETE to DB resulted in 0 or multiple rows returned");
    // }

    public async Task CommitAsync()
    {
        await _db.SaveChangesAsync();
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

    public async Task<Message?> GetMessageAsync(int id)
    {
        return await _msgComp.GetAsync(id);
    }

    public async Task<List<Message>?> GetAllMessagesAsync()
    {
        return await _msgComp.GetAllAsync();
    }

    public async Task<Message?> SendMessageAsync(Message? msg)
    {
        return await _msgComp.AddAsync(msg!);
    }

    public async Task<bool> DeleteMessageAsync(int id)
    {
        return await _msgComp.DeleteAsync(id);
    }
}