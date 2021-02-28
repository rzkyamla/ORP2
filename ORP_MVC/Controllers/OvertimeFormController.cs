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
    public class OvertimeFormController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Emailvalue"] = HttpContext.Session.GetString("email");
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

        [HttpPost]
        public HttpStatusCode CreateOvertimeForm(OvertimeFormViewModels overtimeFormViewModels)
        {
            var httpClient = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(overtimeFormViewModels), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44346/api/OvertimeForm/Apply/", stringContent).Result;
            return result.StatusCode;
        }

        [HttpDelete]
        public HttpStatusCode Delete(int Id)
        {
            var httpClient = new HttpClient();
            var response = httpClient.DeleteAsync("https://localhost:44346/api/detailovertimerequest/" + Id).Result;
            return response.StatusCode;
        }
    }
}
