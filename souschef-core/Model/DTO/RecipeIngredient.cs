namespace souschef_core.Model.DTO;

public record RecipeIngredient
{
    public long IngrId { get; init; }
    
    public string? IngrName { get; init; }
    
    public long MeasId { get; init; }
    
    public string? MeasName { get; init; }
    
    public string? Note { get; init; }
    
    public string? Section { get; init; }
}