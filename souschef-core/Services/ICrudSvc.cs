using souschef_core.Model;

namespace souschef_core.Services;

public interface ICrudSvc<T>
{
    Task<T?> GetAsync(long id);
    Task<List<T>?> GetAllAsync();
    Task<T?> AddAsync(T? msg);
    Task<bool> DeleteAsync(long id);
}