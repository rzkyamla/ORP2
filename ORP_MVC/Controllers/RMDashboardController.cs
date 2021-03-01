using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_MVC.Controllers
{
    public class RMDashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Namevalue"] = HttpContext.Session.GetString("name");
            return View();
        }
    }
}
