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
    public class TrainingProgramController : Controller
    {
        private readonly IConfiguration _config;

        public TrainingProgramController(IConfiguration config)
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
        // GET: TrainingProgramController
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT Id,
               Name, StartDate
            FROM TrainingProgram
        ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<TrainingProgram> trainingprograms = new List<TrainingProgram>();
                    while (reader.Read())
                    {
                      TrainingProgram  trainingprogram = new TrainingProgram
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),

                        };

                        trainingprograms.Add(trainingprogram);
                    }

                    reader.Close();

                    return View(trainingprograms);
                }
            }
        }

        // GET: TrainingProgramController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrainingProgramController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainingProgramController/Create
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

        // GET: TrainingProgramController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrainingProgramController/Edit/5
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

        // GET: TrainingProgramController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrainingProgramController/Delete/5
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
