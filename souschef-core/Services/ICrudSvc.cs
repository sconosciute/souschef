using souschef_core.Model;

namespace souschef_core.Services;

public interface ICrudSvc<T>
{
    Task<T?> GetAsync(long id);
    Task<List<T>?> GetAllAsync();
    Task<T?> AddAsync(T? ent);
    Task<T?> UpdateAsync(T? ent, long id);
    Task<bool> DeleteAsync(long id);
}