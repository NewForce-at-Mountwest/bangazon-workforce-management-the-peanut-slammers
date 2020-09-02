using BangazonWorkforce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.ViewModels
{
    public class AddEmployeeViewModel
    {
        public Employee Employee { get; set; }

        public List<SelectListItem> departments { get; set; } = new List<SelectListItem>();
    }
}
