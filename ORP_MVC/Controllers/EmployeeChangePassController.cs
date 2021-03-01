﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_MVC.Controllers
{
    public class EmployeeChangePassController : Controller
    {
        public IActionResult Index()
        {
            ViewData["NIKValue"] = HttpContext.Session.GetString("nik");
            return View();
        }
    }
}