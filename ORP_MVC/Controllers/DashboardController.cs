using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_MVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Namevalue"] = HttpContext.Session.GetString("name");
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("rolename") == "Admin")
                {
                    return View();
                }
                else if (HttpContext.Session.GetString("rolename") == "Relational Manager")
                {
                    return RedirectToAction("index", "RMDashboard");
                }
                else if (HttpContext.Session.GetString("rolename") == "Supervisor")
                {
                    return RedirectToAction("index", "SupervisorDashboard");
                }
                else
                {
                    return RedirectToAction("index", "EmployeeDashboard");
                }
            }
            return RedirectToAction("index", "HomePage");

        }

        [HttpGet ("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "HomePage");
        }
    }
}
