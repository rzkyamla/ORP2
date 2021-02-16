using Microsoft.IdentityModel.Tokens;
using ORP_API.Repositories.Data;
using ORP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ORP_API.Handler
{
    public class Jwt : IJWTAuthenticationManager
    {
        private readonly string tokenKey;
        public Jwt(string tokenKey)
        {
            this.tokenKey = tokenKey;
        }
        public string Generate(LoginViewModels loginVM)
        {
            if (loginVM != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(tokenKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Name", loginVM.Name),
                    new Claim("Email", loginVM.Email),
                    new Claim(ClaimTypes.Role, "Employee")
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            return "";
        }
    }
    public interface IJWTAuthenticationManager
    {
        string Generate(LoginViewModels loginVM);
    }

}
