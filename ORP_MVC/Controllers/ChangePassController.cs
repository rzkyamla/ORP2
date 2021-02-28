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
    public class ChangePassController : Controller
    {
        public IActionResult Index()
        {
            ViewData["NIKValue"] = HttpContext.Session.GetString("nik");
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("index", "HomePage");
            }
            return View();
        }
        [HttpPut]
        public HttpStatusCode ChangePassword(ChangePasswordViewModels changePasswordViewModels)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(changePasswordViewModels), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync("https://localhost:44346/api/Account/ChangePassword/", content).Result;
            return result.StatusCode;
        }
    }
}
