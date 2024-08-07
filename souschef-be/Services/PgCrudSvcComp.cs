using Microsoft.EntityFrameworkCore;
using souschef_be.models;
using souschef_core.Exceptions;
using souschef_core.Model;

namespace souschef_be.Services;

public class PgCrudSvcComp<T>(ILogger<PgCrudSvcComp<T>> logger, SouschefContext db)
    where T : class, IDbModel
{
    private readonly DbSet<T> _set = db.Set<T>();

    public async Task<T?> GetAsync(long id)
    { 
        logger.LogDebug("Request for {type} ID {id} received", typeof(T), id);
        return await _set.FindAsync(id);
    }

    public async Task<List<T>?> GetAllAsync()
    {
        logger.LogDebug("Request for list of all {type} entities received", typeof(T));
        return await _set.ToListAsync();
    }

    public async Task<T?> AddAsync(T ent)
    {
        logger.LogDebug("Received {type} to add to DB: {entity}", typeof(T), ent);
        var added = (await _set.AddAsync(ent)).Entity;
        var committed = await db.SaveChangesAsync();
        return committed == 1
            ? added
            : throw new DbApiFailureException($"Single add to Db resulted in {committed} rows returned");
    }

    public async Task<bool> DeleteAsync(long id)
    {
        logger.LogDebug("Request to delete {type} ID {id} received", typeof(T), id);
        var toDelete = await _set.FindAsync(id);
        if (toDelete is null)
        {
            logger.LogWarning("API request to delete null entity ignored.");
            return false;
        }

        _set.Remove(toDelete);
        var deleted = await db.SaveChangesAsync();
        return deleted == 1
            ? true
            : throw new DbApiFailureException($"Single DELETE to DB resulted in {deleted} rows returned");
    }
    
    
}