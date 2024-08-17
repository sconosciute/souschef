using Microsoft.EntityFrameworkCore;
using souschef_be.models;
using souschef_core.Model;
using souschef_core.Model.DTO;

namespace souschef_be.Services;

public static class RecipeExtensions
{
    public static async Task<HumanReadableRecipe> ToHumanReadable(this Recipe recipe)
    {
        await using var db = new SouschefContext();
        var tags = db.Set<Tag>();
        
        var ingredients = recipe.IngrRecipes.Select(ir => ir.ToHumanReadable()).ToList();
        return new HumanReadableRecipe
        {
            RecipeId = recipe.RecipeId,
            AuthorId = recipe.Author,
            Public = recipe.Public,
            Name = recipe.Name!,
            Description = recipe.Description!,
            Directions = recipe.Directions!,
            Ingredients = ingredients,
            Tags = await tags.Where(t => recipe.Tags != null && recipe.Tags.Contains(t.TagId)).ToListAsync()
        };
    }

    public static async Task<List<Tag>> GetTagEntities(this Recipe recipe)
    {
        await using var db = new SouschefContext();
        return await db.Set<Tag>().Where(t => recipe.Tags != null && recipe.Tags.Contains(t.TagId)).ToListAsync();
    }
}