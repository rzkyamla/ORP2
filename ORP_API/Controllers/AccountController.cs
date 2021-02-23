using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ORP_API.Base.Controller;
using ORP_API.Handler;
using ORP_API.Models;
using ORP_API.Repositories.Data;
using ORP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        private IConfiguration Configuration;
        public AccountController(AccountRepository accountRepository, IConfiguration Configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.Configuration = Configuration;
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterViewModels registerViewModels)
        {
            if (ModelState.IsValid)
            {
                var data = accountRepository.Register(registerViewModels);
                if (data > 0)
                {
                    return Ok(new { status = "Registration Successed" });
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
        [HttpPut("reset")]
        public ActionResult ResetPassword(RegisterViewModels registerViewModels)
        {
            var data = accountRepository.ResetPassword(registerViewModels.Email);
            return (data > 0) ? (ActionResult)Ok(new { message = "Email has been Sent, password changed", status = "Ok" }) : NotFound(new { message = "Data not exist in our database, please register first", status = 404 });
        }

        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordViewModels changePasswordViewModels)
        {
            var acc = accountRepository.Get(changePasswordViewModels.NIK);
            if (acc != null)
            {
                if (Hashing.ValidatePassword(changePasswordViewModels.OldPassword, acc.Password))
                {
                    var data = accountRepository.ChangePassword(changePasswordViewModels);
                    return Ok(data);
                }
                else
                {
                    return StatusCode(404, new { status = "404", message = "Wrong password" });
                }
            }
            return NotFound();
        }
    }
}
