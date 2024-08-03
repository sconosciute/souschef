using System;
using System.Collections.Generic;

namespace souschef_be.models;

public partial class Tag
{
    public long TagId { get; set; }

    public string? TagName { get; set; }
}
