using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_MVC.Controllers
{
    public class EmployeeRequestController : Controller
    {
        public IActionResult Index()
        {
            ViewData["NIKvalue"] = HttpContext.Session.GetString("nik");
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("rolename") == "Employee")
                {
                    return View();
                }
                return RedirectToAction("index", "Dashboard");
            }
            return RedirectToAction("index", "HomePage");
        }
    }
}
