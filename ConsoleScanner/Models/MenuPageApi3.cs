using System;
using System.Collections.Generic;

namespace ScannerAPIProject.Models;

public partial class MenuPageApi3
{
    public int MemuPageApiId { get; set; }

    public string? ApiUrl { get; set; }

    public string? RedirectUrl { get; set; }

    public int? MenuPageId { get; set; }

    public int? SidaMenuPageId { get; set; }
}
