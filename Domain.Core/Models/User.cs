using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class User
{
    public Guid Appid { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}
