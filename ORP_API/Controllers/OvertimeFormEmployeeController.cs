using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORP_API.Base.Controller;
using ORP_API.Models;
using ORP_API.Repositories.Data;
using ORP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeFormEmployeeController : BaseController<OvertimeFormEmployee, OvertimeFormEmployeeRepository, int>
    {
        private readonly OvertimeFormEmployeeRepository overtimeFormEmployeeRepository;
        public OvertimeFormEmployeeController(OvertimeFormEmployeeRepository overtimeFormEmployeeRepository) : base(overtimeFormEmployeeRepository)
        {
            this.overtimeFormEmployeeRepository = overtimeFormEmployeeRepository;
        }

        [HttpPut("Supervisor/Manage")]
        public ActionResult SupervisorApproval(OvertimeFormEmployee overtimeFormEmployee)
        {
            {
                try
                {
                    var data = overtimeFormEmployeeRepository.SupervisorApproval(overtimeFormEmployee);
                    return Ok(new { status = "Ok" });
                }
                catch (Exception)
                {
                    return StatusCode(500, new { status = "Internal Server Error", errorMessage = "Failed to input the data" });
                }
            }
        }

        [HttpPut("RM/Manage")]
        public ActionResult RelatonalManagerApproval(OvertimeFormEmployee overtimeFormEmployee)
        {
            {
                try
                {
                    var data = overtimeFormEmployeeRepository.RelationalManagerApproval(overtimeFormEmployee);
                    return Ok(new { status = "Ok" });
                }
                catch (Exception)
                {
                    return StatusCode(500, new { status = "Internal Server Error", errorMessage = "Failed to input the data" });
                }
            }
        }

        //[HttpGet("Get/{NIK}")]
        //public ActionResult GetForm(string NIK)
        //{
        //    var data = overtimeFormEmployeeRepository.GetSpecificForm(NIK);
        //    return (data != null) ? (ActionResult)Ok(new { data = data, status = "Ok" }) : NotFound(new { data = data, status = "Not Found", errorMessage = "ID is not identified" });
        //}

        [HttpGet("Employee/History/{NIK}")]
        public ActionResult GetRequest(string NIK)
        {
            var data = overtimeFormEmployeeRepository.GetHistoryRequest(NIK);
            return (data != null) ? (ActionResult)Ok(new { data = data, status = "Ok" }) : NotFound(new { data = data, status = "Not Found", errorMessage = "ID is not identified" });
        }
    }
}
