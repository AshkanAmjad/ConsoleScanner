using ScannerAPIProject.Models;

namespace ScannerAPIProject.Services.Interfaces
{
    public interface IScannerApiServices
    {
        bool ScanAndSaveAllControllersAndApis(out string message,string rootPath);
        List<string> ExtractApiEndpoints(string fileContent);
        List<string> ExtractPopupEndpoints(string fileContent);
        List<string> ExtractRedirects(string fileContent);
        List<string> ExtractApiFromDirectives(List<string> directives);
        IQueryable<MenuPageMapping> GetMappingsQuery();
        void MappingMenuPages();
        void SaveChanges();
    }
}
