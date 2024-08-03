using System;
using System.Collections.Generic;

namespace souschef_be.models;

public partial class Message
{
    public int MsgId { get; set; }

    public string MsgText { get; set; } = null!;
}
