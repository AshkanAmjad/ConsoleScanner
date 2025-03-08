namespace ScannerAPIProject.Models;

public partial class MenuPageApi3
{
    public int MemuPageApiId { get; set; }

    public string ApiUrl { get; set; } = null!;

    public string? RedirectUrl { get; set; }

    public int MenuPageId { get; set; }
}
