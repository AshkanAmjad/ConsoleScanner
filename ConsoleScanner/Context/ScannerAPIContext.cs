using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScannerAPIProject.Models.Entities;

namespace ScannerAPIProject.Context
{
    public class ScannerAPIContext : DbContext
    {
        public virtual DbSet<MenuPage> MenuPages { get; set; }
        public virtual DbSet<MenuPageApi> MenuPageApis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ApiScanner_DB;Integrated Security=True;TrustServerCertificate=true");

    }
}
