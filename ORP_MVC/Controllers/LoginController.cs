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
    public class LoginController : Controller
    {
        [HttpPost]
        public HttpStatusCode Login(LoginViewModels loginViewModels)
        {
            var httpclient = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginViewModels), Encoding.UTF8, "application/json");
            var result = httpclient.PostAsync("https://localhost:44346/api/Auth/Authenticate/", stringContent).Result;
            return result.StatusCode;
        }
    }
}
