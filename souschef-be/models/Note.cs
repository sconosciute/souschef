﻿using System;
using System.Collections.Generic;

namespace souschef_be.models;

public partial class Note
{
    public long? UserId { get; set; }

    public long? RecipeId { get; set; }

    public string? Note1 { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
