namespace souschef_core.Model;

public partial class Tag : IDbModel
{
    public long TagId { get; set; }

    public string? TagName { get; set; }
}
