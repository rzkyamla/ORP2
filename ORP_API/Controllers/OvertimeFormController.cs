using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORP_API.Base.Controller;
using ORP_API.Context;
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
    public class OvertimeFormController : BaseController<OvertimeForm, OvertimeFormRepository, int>
    {
        private readonly OvertimeFormRepository overtimeFormRepository;
        public OvertimeFormController(OvertimeFormRepository overtimeFormRepository) : base(overtimeFormRepository)
        {
            this.overtimeFormRepository = overtimeFormRepository;
        }
        [HttpPost("Apply")]
        public IActionResult AddOvertime(OvertimeFormViewModels overtimeFormViewModels)
        {
            if (ModelState.IsValid)
            {
                var data = overtimeFormRepository.Apply(overtimeFormViewModels);
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
    }
}
