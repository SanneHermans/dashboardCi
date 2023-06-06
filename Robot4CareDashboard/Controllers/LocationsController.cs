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
using Robot4CareDashboard.DatabaseManagers;
using Robot4CareDashboard.Logics;

namespace Robot4CareDashboard.Controllers
{
    public class LocationsController : Controller
    {
        private readonly IConfiguration configuration;

        public LocationsController(IConfiguration Configuration)
        {
            configuration = Configuration;
        }

        public IActionResult Index()
        {
            LocationDatabaseManager locationDatabaseManager = new LocationDatabaseManager(configuration);
            LocationLogics logic = new LocationLogics(locationDatabaseManager);
            List<LocationModel> locations = logic.Get();
            return View(locations);
        }

        [HttpPost]
        public IActionResult Index(int X, int Y, string Name)
        {
            LocationDatabaseManager locationDatabaseManager = new LocationDatabaseManager(configuration);
            LocationLogics logic = new LocationLogics(locationDatabaseManager);
            logic.Create(X, Y, Name);

            List<LocationModel> locations = logic.Get();
            return View(locations);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            LocationDatabaseManager locationDatabaseManager = new LocationDatabaseManager(configuration);
            LocationLogics logic = new LocationLogics(locationDatabaseManager);
            logic.Delete(Id);
            return RedirectToAction("Index", "Locations");
        }
    }
}
