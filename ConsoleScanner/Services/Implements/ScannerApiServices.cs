using System.Text.RegularExpressions;
using ScannerAPIProject.Models;
using ScannerAPIProject.Services.Interfaces;

namespace ScannerAPIProject.Services.Implements
{
    public class ScannerApiServices : IScannerApiServices
    {
        private readonly TotalSchoolContext _context;

        public ScannerApiServices()
        {
            _context = new TotalSchoolContext();
        }

        public bool ScanAndSaveAllControllersAndApis(string rootPath)
        {
            var result = false;

            if (!Directory.Exists(rootPath))
            {
                Console.WriteLine($"Not Found => {rootPath}");
                return result;
            }
            HashSet<MenuPageApi3> menuPageApi = new();

            var jsFiles = Directory.GetFiles(rootPath, "*controller.js", SearchOption.AllDirectories);

            foreach (var jsFile in jsFiles)
            {
                string fileContent = File.ReadAllText(jsFile);
                string controllerName = Path.GetFileNameWithoutExtension(jsFile);

                if (controllerName.EndsWith("Controller"))
                {
                    controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);
                }

                var existingPage = _context.MenuPage3.Where(p => p.ControllerName == controllerName)
                                                     .FirstOrDefault();

                if (existingPage == null)
                {
                    existingPage = new MenuPage3
                    {
                        ControllerName = controllerName
                    };
                    //_context.MenuPage3.Add(existingPage);
                    //SaveChanges();
                }

                var apiUrls = ExtractApiEndpoints(fileContent);
                var redirects = ExtractRedirects(fileContent);
                var popUps = ExtractPopupEndpoints(fileContent);
                apiUrls.AddRange(popUps);

                if (apiUrls.Count > 0)
                {
                    foreach (var api in apiUrls)
                    {
                        string redirectUrl = redirects.FirstOrDefault() ?? string.Empty; // اگر مقدار ریدایرکت یافت نشد، از رشته خالی استفاده کن

                        menuPageApi.Add(new MenuPageApi3
                        {
                            ApiUrl = api,
                            RedirectUrl = redirectUrl,
                            MenuPageId = existingPage.MenuPageId
                        });

                    }
                }
            }

            if (menuPageApi.Count > 0)
            {
                //_context.MenuPageApi3.AddRange(menuPageApi);
                //SaveChanges();
                MappingMenuPages();
                result = true;
            }

            return result;
        }

        public List<string> ExtractApiEndpoints(string fileContent)
        {
            var apiUrls = new List<string>();

            var regexPatterns = new List<string>
            {
               @"['""](\/api\/[\w\/-]+)['""]",
               @"['""](\/api\/[^'""?]+\?[^'""]+)['""]",
               @"`(\/api\/[^`]+)`",
               @"\$scope\.\w+API\s*=\s*['""]([^'""]+)['""]",
               @"['""](api\/[^'""?]+|\/api\/[^'""?]+)['""]",
               @"url:\s*['""](\/?api\/[^\s'""]+)",
               @"api/[^\""?']+"
            };

            foreach (var pattern in regexPatterns)
            {
                var matches = Regex.Matches(fileContent, pattern, RegexOptions.IgnoreCase);
                foreach (Match match in matches)
                {
                    var apiUrl = match.Groups[1].Value.Trim();
                    if (!string.IsNullOrEmpty(apiUrl))
                    {
                        apiUrl = Regex.Replace(apiUrl, @"\?.*", "");
                        apiUrls.Add(apiUrl);
                    }
                }
            }

            return apiUrls.Distinct().ToList();
        }


        public List<string> ExtractPopupEndpoints(string fileContent)
        {
            var popups = new List<string>();

            var regexPatterns = new List<string>
            {
               @"\/Sida\/App\/directives\/[a-zA-Z0-9_]+\.js"
            };

            string basePath = @"C:\Users\reza.o\source\repos\sida-cross-platform2\Pajoohesh.School.Web\wwwroot\";
            var fullPaths = new List<string>();

            foreach (var pattern in regexPatterns)
            {
                var matches = Regex.Matches(fileContent, pattern, RegexOptions.IgnoreCase);

                foreach (Match match in matches)
                {
                    var apiUrl = match.Value.Trim();
                    if (!string.IsNullOrEmpty(apiUrl) && !popups.Contains(apiUrl))
                    {
                        popups.Add(apiUrl);
                    }
                }
            }

            foreach (var item in popups)
            {
                string correctedPath = item.Replace("/", "\\");
                string fullPath = Path.Combine(basePath, correctedPath.TrimStart('\\'));
                fullPaths.Add(fullPath);
            }

            var result = ExtractApiFromDirectives(fullPaths);

            return result;
        }



        public List<string> ExtractRedirects(string fileContent)
        {
            var redirects = new List<string>();

            var regexPatterns = new List<string>
            {
                @"\$state\.go\(['""]([^'"",\s\)]+)['""]\s*\)",   // برای $state.go('stateName') و $state.go("stateName")
                @"\$state\.go\(['""]([^'"",\s\)]+)['""]\s*,",    // برای $state.go('stateName', { ... })
                @"\$state\.go\(`([^`]+)`\)",                    // برای $state.go(`stateName`)
                @"\$state\.go\(\s*([\w\d_]+)\s*\)",             // برای $state.go(stateName) بدون کوتیشن
                @"\$state\.go\(\s*['""]([^'""]+)['""],\s*\{",   // برای $state.go("stateName", {...})
            };

            foreach (var pattern in regexPatterns)
            {
                var matches = Regex.Matches(fileContent, pattern, RegexOptions.IgnoreCase);
                foreach (Match match in matches)
                {
                    var redirectUrl = match.Groups[1].Value.Trim();
                    if (!string.IsNullOrEmpty(redirectUrl))
                    {
                        redirects.Add(redirectUrl);
                    }
                }
            }

            return redirects.Distinct().ToList();
        }

        public List<string> ExtractApiFromDirectives(List<string> directives)
        {
            Regex apiPattern = new Regex(@"(?:\/)?api\/[^\""""?']+[^\.html]", RegexOptions.IgnoreCase);

            string[] jsFiles = directives.ToArray();

            List<string> apiUrls = new();


            foreach (var file in jsFiles)
            {
                string[] lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    var apiMatch = apiPattern.Match(line);
                    if (apiMatch.Success)
                    {
                        string apiRoute = apiMatch.Value
                                                  .Trim();

                        apiRoute = apiRoute.TrimEnd('\"', '\'');

                        if (!apiUrls.Contains(apiRoute))
                        {
                            apiUrls.Add(apiRoute);
                        }
                    }
                }
            }
            return apiUrls;
        }

        public void MappingMenuPages()
        {
            //var mappings = (from menuPage in _context.MenuPages
            //                join menuPage_3 in _context.MenuPage3
            //                on menuPage.State equals menuPage_3.ControllerName
            //                join menuPageApi_3 in _context.MenuPageApi3
            //                on menuPage_3.MenuPageId equals menuPageApi_3.MemuPageApiId
            //                select new MenuPageMapping
            //                {
            //                    MenuPageId = menuPage_3.MenuPageId,
            //                    Title = menuPage_3.ControllerName,
            //                    SidaMenupageId = menuPage.Id
            //                }).ToList();


            var mappings = (from menuPage in _context.MenuPages
                           join menuPage_3 in _context.MenuPage3
                           on menuPage.State equals menuPage_3.ControllerName
                           into menuPageGroup
                           from menuPageJoin in menuPageGroup.DefaultIfEmpty()
                           select new MenuPageMapping
                           {
                               MenuPageId = menuPageJoin.MenuPageId!=null && menuPageJoin.MenuPageId != 0 ? menuPageJoin.MenuPageId :0,
                               Title = menuPage.State!=null? menuPage.State :"",
                               SidaMenupageId = menuPage.Id
                           }).ToList();


            if (mappings != null)
            {
                _context.MenuPageMappings.AddRange(mappings);
                SaveChanges();
            }
        }


        public void SaveChanges()
            => _context.SaveChanges();
    }
}





