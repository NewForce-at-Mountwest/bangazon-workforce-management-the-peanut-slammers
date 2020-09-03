using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class Computer
    {
        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime DecommissionDate { get; set; }

        [Display(Name = "Computer:")]
        public string Make { get; set; }

        public string Manufacturer { get; set; }

    }
}
