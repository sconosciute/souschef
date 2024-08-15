using Microsoft.EntityFrameworkCore;
using souschef_core.Model;
using souschef_core.Model.DTO;
using souschef_core.Services;

namespace souschef_be.Services;

public class UserMetricsService(DbContext db)
{
    private readonly DbContext _db = db;
    public async Task<UserMetrics> GetRecipeCount(int userId)
    {
        var userSet = _db.Set<User>();
        var recipeSet = _db.Set<Recipe>();
        var ratingsSet = _db.Set<Rating>();
        
        var recipeCount = from user in userSet
            join recipe in recipeSet on user.UserId equals recipe.Author
            where user.UserId == userId
            group recipe by user.UserId into grouped
            select new 
            {
                author_username = grouped.Key,
                recipe_count = grouped.Count()
            };
        
        var avgRating = from u in userSet
            join r in recipeSet on u.UserId equals r.Author
            join rt in ratingsSet on r.RecipeId equals rt.RecipeId
            where u.UserId == userId
            group rt by u.UserId into grouped
            select new 
            {
                username = grouped.Key,
                average_rating = grouped.Average(rt => rt.Rating1)
            };
        
        return new UserMetrics
        {
            UserId = userId,
            AvgRating = avgRating{average_rating}
        }
    }
    
    
}