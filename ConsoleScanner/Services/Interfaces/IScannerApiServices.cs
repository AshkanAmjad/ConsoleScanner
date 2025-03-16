
using ScannerAPIProject.Models;

namespace ScannerAPIProject.Services.Interfaces
{
    public interface IScannerApiServices
    {
        bool ScanAndSaveAllControllersAndApis(out string message, string rootPath);
        List<string> ExtractApiEndpoints(string fileContent);
        List<string> ExtractPopupEndpoints(string fileContent);
        List<string> ExtractRedirects(string fileContent);
        IQueryable<MenuPageMapping> GetMappingsQuery();
        void MappingMenuPages();
        void SaveChanges();
    }
}
