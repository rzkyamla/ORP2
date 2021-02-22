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
    public class ForgotController : Controller
    {
        public IActionResult ForgotPassword() 
        {
            return View();
        }
        [HttpPut]
        public HttpStatusCode ForgotPassword(RegisterViewModels registerVM)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync("https://localhost:44346/api/Account/reset/", content).Result;
            return result.StatusCode;
        }
    }
}
