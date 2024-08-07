using Microsoft.EntityFrameworkCore;
using souschef_be.models;
using souschef_core.Exceptions;
using souschef_core.Model;

namespace souschef_be.Services;

/// <summary>
/// Tagging component for <see cref="PgDbService"/>
/// </summary>
/// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to use for this environment </param>
/// <param name="db">Th <see cref="SouschefContext"/>e database context</param>
public class PgTagSvcComp(ILogger<PgTagSvcComp> logger, SouschefContext db) : IBeTagSvc
{
    private readonly ILogger _log = logger;

    public async Task<Tag?> GetTagAsync(long id)
    {
        return await db.Tags.FindAsync(id);
    }

    public async Task<List<Tag>?> GetAllTagsAsync()
    {
        return await db.Tags.ToListAsync();
    }

    public async Task<Tag?> PostTagAsync(Tag tag)
    {
        _log.LogDebug("Received tag to post:{tag}", tag);
        return (await db.Tags.AddAsync(tag)).Entity;
    }

    public async Task<bool> DeleteTagAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task CommitAsync()
    {
        throw new NotImplementedException();
    }
}