using Ecommerce.DAL.Entities.Identites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecoomerce.BLL.Services
{
    
        public interface ITokenService
        {
            public Task<string> CreateToken(AppUser entity, UserManager<AppUser> user);
        }
    
}
