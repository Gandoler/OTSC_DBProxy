using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class MailComprehension
{
    public Guid Appid { get; set; }

    public string Mail { get; set; } = null!;

    public virtual User App { get; set; } = null!;
}
