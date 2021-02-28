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
    public class ManageRoleController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("rolename") == "Admin")
                {
                    return View();
                }
                return RedirectToAction("index", "Dashboard");
            }
            return RedirectToAction("index", "HomePage");
        }
        [HttpPut]
        public HttpStatusCode UpdateRole(RegisterViewModels registerViewModels)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerViewModels), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync("https://localhost:44346/api/employee/", content).Result;
            return result.StatusCode;
        }
        [HttpGet]
        public String Get(int Id)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://localhost:44346/api/employee/" + Id).Result;
            var apiResponse = response.Content.ReadAsStringAsync();
            return apiResponse.Result;
        }
    }
}
