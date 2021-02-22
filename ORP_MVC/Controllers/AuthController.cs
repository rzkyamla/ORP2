using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ORP_API.Context;
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
        private MyContext myContext = new MyContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public HttpStatusCode Register(RegisterViewModels registerVM)
        {
            ViewBag.Id_Customer = new SelectList(myContext.Customer, "Id", "Name");
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44346/api/Account/Register/", content).Result;
            return result.StatusCode;
        }
        [HttpPut]
        public HttpStatusCode ForgotPassword(RegisterViewModels registerVM)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync("https://localhost:44346/api/Account/reset/", content).Result;
            return result.StatusCode;
        }
        [HttpPut]
        public HttpStatusCode ChangePassword(ChangePasswordViewModels changePasswordViewModels)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(changePasswordViewModels), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync("https://localhost:44346/api/Account/ChangePassword/" , content).Result;
            return result.StatusCode;
        }

    }
}
