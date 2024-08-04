using Microsoft.EntityFrameworkCore;
using souschef_be.models;
using souschef_core.Model;

namespace souschef_be.Services;

internal class PgDbService : IBeMessageSvc, IMeasurementSvc
{
    private readonly SouschefContext _db = new();
    public async Task<Message?> GetMessageAsync(int id)
    {
        return await _db.Messages.FindAsync(id);
    }

    public async Task<List<Message>> GetAllMessagesAsync()
    {
        return await _db.Messages.ToListAsync();
    }

    public async Task<Message?> SendMessageAsync(Message? msg)
    {
        var res = (await _db.Messages.AddAsync(msg)).Entity;
        await _db.SaveChangesAsync();
        return res;
    }

    public async Task<bool> DeleteMessageAsync(int id)
    {
        var msg = await _db.Messages.FindAsync(id);
        if (msg is null)
        {
            return false;
        }

        _db.Messages.Remove(msg);
        await _db.SaveChangesAsync();
        return true;
    }

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
}