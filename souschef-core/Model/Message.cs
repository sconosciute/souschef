using System.ComponentModel.DataAnnotations;

namespace souschef_core.Model;

public partial class Message : IDbModel
{
    public long MsgId { get; set; }

    public string? MsgText { get; set; }
}
