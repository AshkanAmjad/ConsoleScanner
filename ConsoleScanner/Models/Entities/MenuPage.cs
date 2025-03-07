using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerAPIProject.Models.Entities
{
    public class MenuPage
    {
        [Key]
        public int MenuPageId { get; set; }
        public string FolderName { get; set; }
        public string ControllerName { get; set; }
        public virtual ICollection<MenuPageApi> MenuPageApis { get; set; }
    }
}
