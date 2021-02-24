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

        [HttpPost("Request")]
        public IActionResult RequestOvertime(OvertimeFormViewModels overtimeFormViewModels)
        {
            if (ModelState.IsValid)
            {
                var data = overtimeFormEmployeeRepository.ApplyRequest(overtimeFormViewModels);
                if (data > 0)
                {
                    return Ok(new { status = "Request Added" });
                }
                else
                {
                    return StatusCode(500, new { status = "Internal server error" });
                }
            }
            else
            {
                return BadRequest(new { status = "Bad request", errorMessage = "Data input is not valid" });
            }
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
    }
}
