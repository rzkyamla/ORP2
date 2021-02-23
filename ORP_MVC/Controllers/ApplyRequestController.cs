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
    public class ApplyRequestController : Controller
    {
        public HttpStatusCode ApplyRequest(RequestViewModels requestViewModels)
        {
            var httpClient = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(requestViewModels), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44346/api/OvertimeFormEmployee/Request/", stringContent).Result;
            return result.StatusCode;
        }
    }
}
