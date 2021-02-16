using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORP_API.Handler;
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
    public class AuthController : ControllerBase
    {
        private readonly AccountRepository accountRepository;
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;

        public AuthController(IJWTAuthenticationManager jWTAuthenticationManager, AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
            this.jWTAuthenticationManager = jWTAuthenticationManager;
        }

        [HttpPost("Login")]
        public LoginViewModels Login([FromBody] LoginViewModels loginViewModels)
        {
            var user = accountRepository.Login(loginViewModels.Email, loginViewModels.Password);
            return user;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginViewModels loginVM)
        {
            var token = jWTAuthenticationManager.Generate(Login(loginVM));
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
