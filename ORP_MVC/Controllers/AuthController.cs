﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ORP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ORP_MVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public HttpStatusCode Register(RegisterViewModels registerVM)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44346/api/Account/Register/", content).Result;
            return result.StatusCode;
        }

    }
}
