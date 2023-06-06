using Robot4CareDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml.Linq;

namespace Robot4CareDashboard.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public DashboardController(IConfiguration Configuration)
        {
            configuration = Configuration;
            connectionString = configuration.GetConnectionString("Robot4Care");
        }
        public IActionResult Index()
        {
            EmergencyModel emergency = new EmergencyModel();
            JamesModel james = new JamesModel();
            LocationModel location = new LocationModel();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT ID, Emergency FROM emergency";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            emergency = new EmergencyModel
                            {
                                Id = (int)sdr["ID"],
                                Emergency = (bool)sdr["Emergency"]
                            };
                        }
                    }
                    con.Close();
                }
                query = "SELECT * FROM james JOIN locations ON james.location_id = locations.ID WHERE james.ID = 1;";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            location = new LocationModel
                            {
                                X = (Double)sdr["X"],
                                Y = (Double)sdr["Y"],
                                Name = (String)sdr["Name"],
                                Id = (int)sdr["Id"]
                            };
                        }
                    }
                    james = new JamesModel { CurrentLocation = location };
                    con.Close();
                }

            }
            DashboardModel dashboard = new DashboardModel
            {
                james = james,
                emergency = emergency
            };
            return View(dashboard);
        }

        [HttpPost]
        public IActionResult Update(bool emergency)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using MySqlCommand command = new MySqlCommand(@"UPDATE emergency SET Emergency=@Emergency", conn);

                conn.Open();

                command.Parameters.AddWithValue("@Emergency", emergency);

                command.ExecuteNonQuery();
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}