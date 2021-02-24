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
        public string Generate(LoginViewModels loginViewModels)
        {
            if (loginViewModels != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(tokenKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Name", loginViewModels.Name),
                    new Claim("Email", loginViewModels.Email),
                    new Claim(ClaimTypes.Role, loginViewModels.RoleName)
                    }),
                    Expires = DateTime.UtcNow.AddMonths(12),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            return null;
        }
    }
    public interface IJWTAuthenticationManager
    {
        string Generate(LoginViewModels loginViewModels);
    }

}
