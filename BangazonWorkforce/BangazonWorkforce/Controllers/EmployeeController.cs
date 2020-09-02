using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BangazonWorkforce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BangazonWorkforce.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //Sql query with join statements to include an employee's department name, assigned computer
                    //and past/previous trainings
                    cmd.CommandText = @"
                         SELECT e.Id, e.FirstName, e.LastName, d.Name, c.Make, c.Manufacturer, t.Name
                            FROM Employee e JOIN Department d 
                            ON e.DepartmentId = d.Id 
                            JOIN ComputerEmployee x ON e.Id = x.EmployeeId
                            JOIN Computer c ON x.ComputerId = c.Id
                            JOIN EmployeeTraining y ON e.Id = y.EmployeeId
                            JOIN TrainingProgram t ON y.TrainingProgramId = t.Id
                            WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Employee employee = null;
                    

                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Department = new Department
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            },
                            Computer = new Computer
                            {
                                Make = reader.GetString(reader.GetOrdinal("Make")),
                                Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer"))
                            },
                            TrainingProgram = new TrainingProgram
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }
                        };
                        

                    }
                    reader.Close();

                    return View(employee);
                }
            }
        }


        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
