using souschef_core.Model.DTO;

namespace souschef_core.Model;

public class IngrRecipe : IDbModel, IHumanFriendly<RecipeIngredient>
{
    public long RecipeId { get; set; }

    public long IngrId { get; set; }

    public decimal? Quantity { get; set; }

    public long Measurement { get; set; }

    public string? Note { get; set; }

    public string? Section { get; set; }

    public virtual Ingredient Ingr { get; set; } = null!;

    public virtual Measurement? MeasurementNavigation { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;
    public RecipeIngredient ToHumanReadable()
    {
        return new RecipeIngredient
        {
            IngrId = IngrId,
            IngrName = Ingr.Name,
            MeasId = Measurement,
            MeasName = MeasurementNavigation!.Name,
            Note = Note,
            Section = Section
        };
    }
}
