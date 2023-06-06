using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Robot4CareDashboard.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics;
using System.Data;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;

namespace Robot4CareDashboard.Controllers
{
    public class RoutesController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public RoutesController(IConfiguration Configuration)
        {
            configuration = Configuration;
            connectionString = configuration.GetConnectionString("Robot4Care");
        }
        public IActionResult Index()
        {
            List<RouteModel> locations = new List<RouteModel>();
            //string constr = System.Configuration.ConfigurationManager.ConnectionStrings["Robot4Care"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT ID, Name FROM routes";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            locations.Add(new RouteModel
                            {
                                Name = (String)sdr["Name"],
                                Id = (int)sdr["ID"]
                            });
                        }
                    }
                    con.Close();
                }
            }
            return View(locations);
        }

        [HttpPost]
        public IActionResult Index(string Name)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using MySqlCommand command = new MySqlCommand(@"INSERT INTO routes(Name) VALUES(@Name);", conn);
                conn.Open();

                command.Parameters.AddWithValue("@Name", Name);

                command.ExecuteScalar();
            }
            List<RouteModel> routes = new List<RouteModel>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT ID, Name FROM routes";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            routes.Add(new RouteModel
                            {
                                Name = (String)sdr["Name"],
                                Id = (int)sdr["Id"]
                            });
                        }
                    }
                    con.Close();
                }
            }
            return View(routes);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using MySqlCommand command = new MySqlCommand(@"DELETE from routes WHERE ID=@Id", conn);

                conn.Open();

                command.Parameters.AddWithValue("@ID", Id);

                command.ExecuteScalar();
            }
            List<RouteModel> routes = new List<RouteModel>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT ID, X, Y, Name FROM routes";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            routes.Add(new RouteModel
                            {
                                Name = (String)sdr["Name"],
                                Id = (int)sdr["Id"]
                            });
                        }
                    }
                    con.Close();
                }
            }
            return RedirectToAction("Index", "Routes");
        }
    }
}
