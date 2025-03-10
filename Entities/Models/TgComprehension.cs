using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class TgComprehension
{
    public Guid Appid { get; set; }

    public long? TgId { get; set; }

    public virtual User App { get; set; } = null!;
}
