using BangazonWorkforce.Models;
using BangazonWorkforce.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

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
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                SELECT e.Id,
                e.FirstName,
                e.LastName,
                e.DepartmentId,
                d.Name
                FROM Employee e
                LEFT JOIN Department d on e.DepartmentId = d.Id
                ";
                        SqlDataReader reader = cmd.ExecuteReader();

                        List<Employee> employees = new List<Employee>();
                        while (reader.Read())
                        {
                            Employee employee = new Employee
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Department = new Department
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Name"))
                                }
                            };

                            employees.Add(employee);
                        }

                        reader.Close();

                        return View(employees);
                    }
                }
            }
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    // Select all the departments
                    cmd.CommandText = @"SELECT Department.Id, Department.Name FROM Department";


            SqlDataReader reader = cmd.ExecuteReader();

            // Create a new instance of our view model
            AddEmployeeViewModel viewModel = new AddEmployeeViewModel();
            while (reader.Read())
            {
                // Map the raw data to our department model
                Department department = new Department
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name"))
                };


                        // Use the info to build our SelectListItem
                        SelectListItem departmentOptionTag = new SelectListItem()
                        {
                            Text = department.Name,
                            Value = department.Id.ToString()
                        };

                        // Add the select list item to our list of dropdown options
                        viewModel.departments.Add(departmentOptionTag);

                    }

                    reader.Close();


                    // send it all to the view
                    return View(viewModel);
                }
            }
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddEmployeeViewModel viewModel)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        //query to choose information you want to insert into employree
                        cmd.CommandText = @"INSERT INTO Employee
                ( FirstName, LastName, DepartmentId, IsSupervisor )
                VALUES
                ( @firstName, @lastName, @departmentId, @issupervisor )";
                        cmd.Parameters.Add(new SqlParameter("@firstName", viewModel.Employee.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@lastName", viewModel.Employee.LastName));
                        cmd.Parameters.Add(new SqlParameter("@departmentId", viewModel.Employee.DepartmentId));
                        cmd.Parameters.Add(new SqlParameter("@issupervisor", viewModel.Employee.IsSupervisor));
                        cmd.ExecuteNonQuery();


                        //doesnt work yet but is supposed to redirect you back to the index view
                        return RedirectToAction(nameof(Index));
                    }
                }
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
