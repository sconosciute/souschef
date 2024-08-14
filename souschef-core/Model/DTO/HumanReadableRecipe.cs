namespace souschef_core.Model.DTO;

public record HumanReadableRecipe
{
    public long RecipeId { get; init; }

    public long? AuthorId { get; init; }
    
    public string? AuthorName { get; init; }

    public required bool Public { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public List<Tag>? Tags { get; init; }

    public required string Directions { get; init; }
    
    public required List<RecipeIngredient> Ingredients { get; init; }
}