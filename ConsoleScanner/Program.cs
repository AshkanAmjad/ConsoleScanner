using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScannerAPIProject.Context;
using ScannerAPIProject.Services;
using ScannerAPIProject.Services.Implements;
using ScannerAPIProject.Services.Interfaces;

namespace ScannerAPIProject
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {                
                bool result = false;
                Console.WriteLine("1. Automatic Scan");
                Console.WriteLine("2. Exit");
                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    try
                    {
                        IScannerApiServices services = new ScannerApiServices();
                        string rootPath = @"C:\Users\reza.o\source\repos\sida-cross-platform2\Pajoohesh.School.Web\wwwroot\Sida\App\views";
                        result = services.ScanAndSaveAllControllersAndApis(rootPath);

                        if (result)
                        {
                            services.SaveChanges();
                            Console.WriteLine("Successfully operation.");
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                else if (choice == "2")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
        }
    }
}
