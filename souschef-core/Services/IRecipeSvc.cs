using souschef_core.Model.DTO;
using souschef_core.Model;

namespace souschef_core.Services;

public interface IRecipeSvc<T>
{
    Task<HumanReadableRecipe?> GetAsync(long id);

    Task<T?> GetRecAsync(long id);
    
    Task<List<HumanReadableRecipe>?> GetAllAsync();
    Task<T?> AddAsync(T? ent);
    Task<T?> UpdateAsync(T? ent, long id);
    Task<bool> DeleteAsync(long id);
}