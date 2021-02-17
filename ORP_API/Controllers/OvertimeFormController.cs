using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORP_API.Base.Controller;
using ORP_API.Models;
using ORP_API.Repositories.Data;
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
        public IActionResult AddOvertime([FromBody] OvertimeForm overtimeForm)
        {
            try
            {
                var data = overtimeFormRepository.Create(overtimeForm);
                return Ok(new { data = data, status = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
