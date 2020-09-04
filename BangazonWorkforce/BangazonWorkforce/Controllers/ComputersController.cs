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
    public class ComputersController : Controller
    {
        private readonly IConfiguration _config;

        public ComputersController(IConfiguration config)
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

        //TODO:: MAKE COMPUTER NAME A HYPERLINK THAT LEADS TO DETAILS RATHER THAN A SEPARATE BUTTON
        //TODO:: DIRECT USER TO A MESSAGE STATING THEIR 

        // GET: ComputersController
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT c.Id,
                c.Make AS 'Computer'
            FROM Computer c
        ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Computer> computers = new List<Computer>();
                    while (reader.Read())
                    {
                        Computer computer = new Computer
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Make = reader.GetString(reader.GetOrdinal("Computer"))
                        };

                        computers.Add(computer);
                    }

                    reader.Close();

                    return View(computers);
                }
            }
        }

        // GET: ComputersController/Details/5
        public ActionResult Details(int id)
        {
            using (SqlConnection conn = Connection)
            {
            

            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                        SELECT
                            Id, Make, Manufacturer, PurchaseDate
                        FROM Computer
                        WHERE Id = @id";
                cmd.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = cmd.ExecuteReader();

                Computer computer = null;

                if (reader.Read())
                {
                    computer = new Computer
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Make = reader.GetString(reader.GetOrdinal("Make")),
                        Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer")),
                        PurchaseDate = reader.GetDateTime(reader.GetOrdinal("PurchaseDate")).ToString(),
                    };
                }
                reader.Close();

                return View(computer);
            }
        }
    }

        // GET: ComputersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComputersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Computer computer)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO Computer
                ( Manufacturer, Make, PurchaseDate )
                VALUES
                ( @Manufacturer, @Make, @PurchaseDate )";
                        cmd.Parameters.Add(new SqlParameter("@Manufacturer", computer.Manufacturer));
                        cmd.Parameters.Add(new SqlParameter("@Make", computer.Make));
                        cmd.Parameters.Add(new SqlParameter("@PurchaseDate", computer.PurchaseDate));
                        cmd.ExecuteNonQuery();

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ComputersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ComputersController/Edit/5
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

        // GET: ComputersController/Delete/5
        public ActionResult Delete(int id, IFormCollection collection)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT
                            Id, Make, Manufacturer, PurchaseDate
                        FROM Computer
                        WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Computer computer = null;

                    if (reader.Read())
                    {
                        computer = new Computer
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Make = reader.GetString(reader.GetOrdinal("Make")),
                            Manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer")),
                            PurchaseDate = reader.GetDateTime(reader.GetOrdinal("PurchaseDate")).ToString()

                        };
                    }
                    reader.Close();

                    return View(computer);
                }
            }
        }

        // POST: ComputersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM Computer WHERE Id = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        throw new Exception("No rows affected");
                    }
                }
            }
            catch
            {
                return RedirectToAction(nameof(DeleteError));
            }
        }

        public new ActionResult DeleteError()
        {
            return View();
        }
    }
}
