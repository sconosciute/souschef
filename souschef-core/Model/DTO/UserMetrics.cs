namespace souschef_core.Model.DTO;

public class UserMetrics
{
    public long UserId { get; init; }
    
    public System.Nullable<double> AvgRating { get; init; }
    
    public long NumRecipe { get; init; }
}