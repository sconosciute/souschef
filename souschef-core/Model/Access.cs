namespace souschef_core.Model;

public partial class Access : IDbModel
{
    public long UserId { get; set; }

    public long RecipeId { get; set; }

    public bool? View { get; set; }

    public bool? Comment { get; set; }

    public bool? Edit { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
