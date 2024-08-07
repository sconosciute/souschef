using souschef_core.Model;

namespace souschef_core.Services;

public interface ITagSvc
{
    Task<Tag?> GetTagAsync(long id);
    Task<List<Tag>?> GetAllTagsAsync();
    Task<Tag?> PostTagAsync(Tag tag);
    Task<bool> DeleteTagAsync(long id);
}