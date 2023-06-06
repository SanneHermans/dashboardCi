using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Robot4CareDashboard.Controllers
{
    public class JamesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
