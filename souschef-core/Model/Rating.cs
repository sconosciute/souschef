namespace souschef_core.Model;

public partial class Rating : IDbModel
{
    public long UserId { get; set; }

    public long RecipeId { get; set; }

    public int? Rating1 { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
