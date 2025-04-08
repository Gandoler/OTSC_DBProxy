// <copyright file="TgComprehension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Models;

using System;
using System.Collections.Generic;

public partial class TgComprehension
{
    public Guid Appid { get; set; }

    public long TgId { get; set; }

    public virtual User App { get; set; } = null!;
}
