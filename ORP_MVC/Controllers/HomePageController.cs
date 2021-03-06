﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public HttpStatusCode Register(RegisterViewModels registerViewModels)
        {
            var httpClient = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(registerViewModels), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44346/api/Account/Register/", stringContent).Result;
            return result.StatusCode;
        }
        
    }
}
