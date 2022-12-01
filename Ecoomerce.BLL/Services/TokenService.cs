using Ecommerce.DAL.Entities.Identites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecoomerce.BLL.Services
{
    public class TokenService :ITokenService
    {

        private readonly IConfiguration _config;
        
        public TokenService(IConfiguration config )
        {

            _config = config;
            
        }



        public async System.Threading.Tasks.Task<string> CreateToken(AppUser entity, UserManager<AppUser> userManager)
        {
            
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, entity.Email));
            claims.Add(new Claim(ClaimTypes.GivenName, entity.Id));

            var userRoles = await userManager.GetRolesAsync(entity);
            if (userRoles != null)
                foreach (var item in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }
            var key = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(_config["JWT:key"]));
            var token = new JwtSecurityToken(
                issuer: _config["JWT:validIssuer"],
                audience: _config["JWT:validAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_config["JWT:durationInDays"])),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
