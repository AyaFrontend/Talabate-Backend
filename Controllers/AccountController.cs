using AutoMapper;
using Ecommerce.DAL.Entities.Identites;
using Ecommerce.DTO;
using Ecommerce.Errors;
using Ecommerce.Extensions;
using Ecoomerce.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
  
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _ueseManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> ueseManager ,
            SignInManager<AppUser> singinManager , IMapper mapper , ITokenService tokenService)
        {
            _ueseManager = ueseManager;
            _signInManager = singinManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerData)
        {
           if(ModelState.IsValid)
           {
                if (IsEmailExist(registerData.Email).Result.Value)
                    return BadRequest(new ApiValidationResponse() { errors = new[] { "that email is aready in use" } });


                var findUserByEmail = await _ueseManager.FindByEmailAsync(registerData.Email);
                if (findUserByEmail != null)
                    return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "This Email already is exist"));
                var user = _mapper.Map<RegisterDto, AppUser>(registerData);
                user.UserName = registerData.Email.Split("@")[0];
                var result = await _ueseManager.CreateAsync(user, registerData.Password);
                if (!result.Succeeded)
                    return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));
              
                return Ok(new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token =await _tokenService.CreateToken(user, _ueseManager)
                }) ;
            }
            return BadRequest(new ApiValidationResponse());   
           
           
        }
  
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            if(ModelState.IsValid)
            {
                var user = await _ueseManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                    return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized));
                var res = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password , false);
                if(!res.Succeeded)
                    return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized));

               

                return Ok(new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token =  await _tokenService.CreateToken(user, _ueseManager)
                });

            }
            return BadRequest(new ApiValidationResponse());
        }
   
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("currentuser")]

        public async Task<ActionResult<UserDto>> CurrentUser()
        {
            var user = User;
            var email = user.FindFirstValue(ClaimTypes.Email);
            var userData = await _ueseManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = userData.DisplayName,
                Email = userData.Email,
                Token = await _tokenService.CreateToken(userData, _ueseManager)
            }); ;
        }

        [Authorize(AuthenticationSchemes  = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("userAddress")]

        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _ueseManager.FindByEmailEagerAsync(email);

            return Ok(_mapper.Map<Address, AddressDto>( user.Address));
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("newaddress")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userData = await _ueseManager.FindByEmailEagerAsync(email);
            userData.Address = _mapper.Map<AddressDto, Address>(address);
            var result = await _ueseManager.UpdateAsync(userData);

            if (!result.Succeeded)
                return BadRequest(new ApiValidationResponse() { errors = new[] { "An error occured with updating" } });

            return Ok(address);
                 
        }


        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> IsEmailExist([FromQuery] string Email)
        {
            return  await _ueseManager.FindByEmailAsync(Email) != null;
        }
    }


      

}
