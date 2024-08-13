using Microsoft.EntityFrameworkCore;
using souschef_core.Model;
using souschef_core.Services;

namespace souschef_be.Services;

public class UserService(ILogger<PgCrudSvcComponent<User>> logger, DbContext db) : PgCrudSvcComponent<User>(logger, db)
{
    private readonly DbContext _db = db;

    public async Task<User?> QueryByUsername(string username)
    {
        ArgumentNullException.ThrowIfNull(username);
        return await _db.Set<User>()
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();
    }
}