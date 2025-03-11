namespace ScannerAPIProject.Models;

public partial class MenuPageApi3
{
    public int MemuPageApiId { get; set; }

    public int SidaMenuPageId { get; set; }
    public string? State { get; set; }

    public string ApiUrl { get; set; } = null!;

    public string? RedirectUrl { get; set; }

}
