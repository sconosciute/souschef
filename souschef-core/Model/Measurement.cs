namespace souschef_core.Model;

public partial class Measurement
{
    public long MeasId { get; set; }

    public string? Name { get; set; }

    public decimal UnitMeasure { get; set; }
    
    public MeasureType Type { get; set; }

    public virtual ICollection<IngrRecipe> IngrRecipes { get; set; } = new List<IngrRecipe>();
}

public enum MeasureType
{
    Volume,
    Weight,
    Unit
}
