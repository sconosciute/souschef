﻿using System;
using System.Collections.Generic;

namespace souschef_be.models;

public partial class Rating
{
    public long UserId { get; set; }

    public long RecipeId { get; set; }

    public int? Rating1 { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
