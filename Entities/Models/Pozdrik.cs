using System;
using System.Collections.Generic;

namespace Entities.Models;

public partial class Pozdrik
{
    public int IdPozdr { get; set; }

    public string? Interest { get; set; }

    public string? Pozhelanie { get; set; }

    public string? Textpozdr { get; set; }
}
