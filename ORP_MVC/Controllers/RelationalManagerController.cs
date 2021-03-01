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
    public class RelationalManagerController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") != null)
            {
                if (HttpContext.Session.GetString("rolename") == "Relational Manager")
                {
                    return View();
                }
                return RedirectToAction("index", "Dashboard");
            }
            return RedirectToAction("index", "HomePage");
            /* return View();*/
        }

        [HttpGet]
        public String Get(int Id)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://localhost:44346/api/OvertimeFormEmployee/" + Id).Result;
            var apiResponse = response.Content.ReadAsStringAsync();
            return apiResponse.Result;
        }

        [HttpPut]
        public HttpStatusCode RelationalManagerApproval(RelationalManagerViewModels relationalManagerViewModels)
        {
            var httpClient = new HttpClient();

            StringContent content = new StringContent(JsonConvert.SerializeObject(relationalManagerViewModels), Encoding.UTF8, "application/json");

            var result = httpClient.PutAsync("https://localhost:44346/api/OvertimeFormEmployee/SubmitApprovedRM/", content).Result;
            return result.StatusCode;
        }
        [HttpPut]
        public HttpStatusCode RelationalManagerRejected(RelationalManagerViewModels relationalManagerViewModels)
        {
            var httpClient = new HttpClient();

            StringContent content = new StringContent(JsonConvert.SerializeObject(relationalManagerViewModels), Encoding.UTF8, "application/json");

            var result = httpClient.PutAsync("https://localhost:44346/api/OvertimeFormEmployee/SubmitRejectRM/", content).Result;
            return result.StatusCode;
        }
    }
}
