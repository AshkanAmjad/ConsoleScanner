namespace ScannerAPIProject.Services.Interfaces
{
    public interface IScannerApiServices
    {
        bool ScanAndSaveAllControllersAndApis(string rootPath);
        List<string> ExtractApiEndpoints(string fileContent);
        List<string> ExtractPopupEndpoints(string fileContent);
        List<string> ExtractRedirects(string fileContent);
        List<string> ExtractApiFromDirectives(List<string> directives);
        void MappingMenuPages();
        void SaveChanges();
    }
}
