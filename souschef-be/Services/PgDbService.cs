using souschef_be.models;
using souschef_core.Model;

namespace souschef_be.Services;

internal class PgDbService : IBeMessageSvc, IMeasurementSvc
{
    private readonly SouschefContext _db = new();

    public Message? GetMessage(int id)
    {
        return _db.Messages.Find(id);
    }

    public List<Message> GetAllMessages()
    {
        try
        {
            return _db.Messages.ToList();
        }
        catch (ArgumentNullException e)
        {
            return [];
        }
    }

    public Message SendMessage(Message msg)
    {
        var res = _db.Messages.Add(msg).Entity;
        return res;
    }

    public bool DeleteMessage(int id)
    {
        var msg = _db.Messages.Find(id);
        if (msg is null)
        {
            return false;
        }
        _db.Messages.Remove(msg);
        return true;
    }

    public void Commit()
    {
        _db.SaveChanges();
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