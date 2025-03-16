using System;
using System.Collections.Generic;

namespace ScannerAPIProject.Models;

public partial class MenuPage
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public int Code { get; set; }

    public string Title { get; set; } = null!;

    public bool Status { get; set; }

    public string? State { get; set; }

    public string? Icon { get; set; }

    public bool IsLastLevel { get; set; }

    public string? Controller { get; set; }

    public int Category { get; set; }

    public string? State2 { get; set; }

    public string? Path { get; set; }
}
