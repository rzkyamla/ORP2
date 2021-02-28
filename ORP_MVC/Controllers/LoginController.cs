using Microsoft.AspNetCore.Http;
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
    public class LoginController : Controller
    {
        [HttpPost]
        public HttpStatusCode Login(LoginViewModels loginViewModels)
        {
            HttpContext.Session.SetString("nik", loginViewModels.NIK);
            HttpContext.Session.SetString("email", loginViewModels.Email);
            var httpclient = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginViewModels), Encoding.UTF8, "application/json");
            var result = httpclient.PostAsync("https://localhost:44346/api/Auth/Login/", stringContent).Result;
            return result.StatusCode;
        }
    }
}
