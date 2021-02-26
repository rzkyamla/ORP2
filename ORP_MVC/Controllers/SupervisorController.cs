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
    public class SupervisorController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
        public HttpStatusCode SupervisorApproval(SupervisorApprovalViewModels supervisorApprovalViewModels)
        {
            var httpClient = new HttpClient();

            StringContent content = new StringContent(JsonConvert.SerializeObject(supervisorApprovalViewModels), Encoding.UTF8, "application/json");

            var result = httpClient.PutAsync("https://localhost:44346/api/OvertimeFormEmployee/Supervisor/Manage", content).Result;
            return result.StatusCode;
        }
    }
}
