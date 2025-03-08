namespace ScannerAPIProject.Models;

public partial class MenuPageMapping
{
    public int MenuPageMappingId { get; set; }

    public string Title { get; set; } = null!;

    public int MenuPageId { get; set; }

    public int SidaMenupageId { get; set; }
}
