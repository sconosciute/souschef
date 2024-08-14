using souschef_core.Model.DTO;

namespace souschef_core.Model;

public partial class Recipe : IDbModel, IHumanFriendly<HumanReadableRecipe>
{
    public long RecipeId { get; set; }

    public long? Author { get; set; }

    public bool Public { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public List<long> Tags { get; set; }

    public string? Directions { get; set; }

    public virtual ICollection<Access> Accesses { get; set; } = new List<Access>();

    public virtual User? AuthorNavigation { get; set; }

    public virtual ICollection<IngrRecipe> IngrRecipes { get; set; } = new List<IngrRecipe>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public HumanReadableRecipe ToHumanReadable()
    {
        var ingredients = IngrRecipes.Select(ir => ir.ToHumanReadable()).ToList();
        return new HumanReadableRecipe
        {
            RecipeId = RecipeId,
            AuthorId = Author,
            Public = Public,
            Name = Name!,
            Description = Description!,
            Directions = Directions!,
            Ingredients = ingredients
            //TODO: Figure out wtf to do about the tags
        };
    }
}
