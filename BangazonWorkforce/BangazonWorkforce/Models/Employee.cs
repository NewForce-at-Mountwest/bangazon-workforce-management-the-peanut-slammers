using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        public int DepartmentId { get; set; }

        public bool IsSupervisor { get; set; }

        [Display(Name = "Department:")]
        public Department Department
        {
            get; set;
        }

        [Display(Name = "Training Program:")]
        public List<TrainingProgram> TrainingPrograms
        {
            get; set;
        } = new List<TrainingProgram>();

        [Display(Name = "Computer:")]
        public Computer Computer
        {
            get; set;
        }
    }
}
