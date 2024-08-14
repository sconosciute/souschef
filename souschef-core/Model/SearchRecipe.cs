namespace souschef_core.Model;

public class SearchRecipe
{
    public string keyword { get; set; }
    
    public List<long> tags { get; set; }
}