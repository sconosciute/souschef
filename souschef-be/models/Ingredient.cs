using System;
using System.Collections.Generic;

namespace souschef_be.models;

public partial class Ingredient
{
    public long IngrId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public List<long>? Tags { get; set; }

    public virtual ICollection<IngrRecipe> IngrRecipes { get; set; } = new List<IngrRecipe>();
}
