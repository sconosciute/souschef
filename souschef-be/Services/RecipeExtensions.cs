using Microsoft.EntityFrameworkCore;
using souschef_be.models;
using souschef_core.Model;
using souschef_core.Model.DTO;

namespace souschef_be.Services;

public static class RecipeExtensions
{
    public static HumanReadableRecipe ToHumanReadable(this Recipe recipe)
    {
        var db = new SouschefContext();
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
            Tags = tags.Where(t => recipe.Tags != null && recipe.Tags.Contains(t.TagId)).ToListAsync().Result
        };
    }
}