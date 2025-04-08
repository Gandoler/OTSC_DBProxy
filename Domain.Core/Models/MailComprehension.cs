// <copyright file="MailComprehension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Models;

using System;
using System.Collections.Generic;

public partial class MailComprehension
{
    public Guid Appid { get; set; }

    public string Mail { get; set; } = null!;

    public virtual User App { get; set; } = null!;
}
