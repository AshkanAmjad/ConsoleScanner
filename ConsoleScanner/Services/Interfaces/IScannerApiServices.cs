using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerAPIProject.Services.Interfaces
{
    public interface IScannerApiServices
    {
        bool ScanAndSaveAllControllersAndApis(string rootPath);
        List<string> ExtractApiEndpoints(string fileContent);
        List<string> ExtractPopupEndpoints(string fileContent);
        List<string> ExtractRedirects(string fileContent);
        List<string> ExtractApiFromDirectives(List<string> directives);
        void SaveChanges();
    }
}
