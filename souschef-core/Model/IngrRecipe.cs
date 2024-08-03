namespace souschef_core.Model;

public partial class IngrRecipe
{
    public long RecipeId { get; set; }

    public long IngrId { get; set; }

    public decimal? Quantity { get; set; }

    public long? Measurement { get; set; }

    public string? Note { get; set; }

    public string? Section { get; set; }

    public virtual Ingredient Ingr { get; set; } = null!;

    public virtual Measurement? MeasurementNavigation { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;
}
