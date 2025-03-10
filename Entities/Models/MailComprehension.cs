using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class MailComprehension
{
    public Guid Appid { get; set; }

    public string Mail { get; set; } = null!;

    public virtual User App { get; set; } = null!;
}
