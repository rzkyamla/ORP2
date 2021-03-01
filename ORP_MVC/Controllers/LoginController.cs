using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ORP_API.Context;
using ORP_API.Repositories.Data;
using ORP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ORP_MVC.Controllers
{
    public class LoginController : Controller
    {
        public IConfiguration Configuration { get; }

        public LoginController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        [HttpPost]
        public HttpStatusCode Login(LoginViewModels loginViewModels)
        {
            LoginViewModels tempResult = null;
            string connectStr = Configuration.GetConnectionString("MyConnection");

            using (IDbConnection db = new SqlConnection(connectStr))
            {
                string readSp = "sp_retrieve_login";
                var parameter = new { Email = loginViewModels.Email, Password = loginViewModels.Password };
                tempResult = db.Query<LoginViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            HttpContext.Session.SetString("nik", tempResult.NIK);
            HttpContext.Session.SetString("name", tempResult.Name);
            HttpContext.Session.SetString("customerid", tempResult.CustomerName);
            HttpContext.Session.SetString("email", loginViewModels.Email);
            HttpContext.Session.SetString("rolename", tempResult.RoleName);
            var httpclient = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginViewModels), Encoding.UTF8, "application/json");
            var result = httpclient.PostAsync("https://localhost:44346/api/Auth/Login/", stringContent).Result;
            return result.StatusCode;
        }
    }
}
