using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Robot4CareDashboard.Controllers
{
    public class EmergencyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
