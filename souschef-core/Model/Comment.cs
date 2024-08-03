namespace souschef_core.Model;

public partial class Comment
{
    public long? UserId { get; set; }

    public long? RecipeId { get; set; }

    public string? Comment1 { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
