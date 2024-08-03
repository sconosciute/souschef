using System;
using System.Collections.Generic;

namespace souschef_be.models;

public partial class Recipe
{
    public long RecipeId { get; set; }

    public long? Author { get; set; }

    public bool? Public { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public List<long>? Tags { get; set; }

    public string? Directions { get; set; }

    public virtual ICollection<Access> Accesses { get; set; } = new List<Access>();

    public virtual User? AuthorNavigation { get; set; }

    public virtual ICollection<IngrRecipe> IngrRecipes { get; set; } = new List<IngrRecipe>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
