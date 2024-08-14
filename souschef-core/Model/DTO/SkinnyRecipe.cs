namespace souschef_core.Model.DTO;

public class SkinnyRecipe
{
    public long id { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
    public List<string> tags { get; set; }
}