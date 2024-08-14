namespace souschef_core.Model.DTO;

public class ThinRecipe
{
    public long id { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
    public List<Tag> tags { get; set; }
}