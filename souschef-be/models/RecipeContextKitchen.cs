using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace souschef_be.models;

/// <summary>
/// Cooks you up a tasty new database context!
/// </summary>
public class RecipeContextKitchen : IDesignTimeDbContextFactory<RecipeAppContext>
{
    public RecipeAppContext CreateDbContext(string[] args)
    {
        return new RecipeAppContext();
    }
}