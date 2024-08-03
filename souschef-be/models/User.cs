using System;
using System.Collections.Generic;

namespace souschef_be.models;

public partial class User
{
    public long UserId { get; set; }

    public byte[]? Photo { get; set; }

    public string? Username { get; set; }

    public string? DisplayName { get; set; }

    public string? Email { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public virtual ICollection<Access> Accesses { get; set; } = new List<Access>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
