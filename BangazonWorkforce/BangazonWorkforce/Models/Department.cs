﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Budget { get; set; }

        public List<Employee> employees { get; set; } = new List<Employee>();

       public int NumberOfEmployees { get; set; }


    }
}
