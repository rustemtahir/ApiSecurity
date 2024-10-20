using JalilApiSecurity.Entities;
using JalilApiSecurity.Models;
using JalilApiSecurity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JalilApiSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AuthController(UserManager<AppUser> userManager, TokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm]LoginModel loginModel)
        {
            var usr=await _userManager.FindByEmailAsync(loginModel.Email);
            if (usr == null)
            return BadRequest(new
            { 
                 isSucceeded=false,
                 message="Email or password is wrong"
            
            });
            var isPassMatch = await _userManager.CheckPasswordAsync(usr, loginModel.Password);
            if (!isPassMatch)
                return BadRequest(new
                {
                    isSucceeded = false,
                    message = "Email or password is wrong"

                });
            var token =  _tokenService.GenerateToken(usr,"User", DateTime.Now.AddHours(1));
            return Ok(new
            {
                isSucceeded=true,
                message ="Login Successful",
                accessToken=token
            });
        }

      

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm]RegisterModel registerModel) 
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var usr = new AppUser()
            {
                
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                Brithdate = registerModel.BrithDate,
                UserName = registerModel.UserName
            };
           var res= await _userManager.CreateAsync(usr, registerModel.Password);
            var err = string.Join(",", res.Errors.Select(e => e.Description));

            await _userManager.AddToRoleAsync(usr, "admin");
            return Ok(new
            {
                isSucceded = res.Succeeded,
                message = res.Succeeded ? "Registration successful" :$"There  were some problem {err}"
            });
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole admin = new()
        //    {
        //        Name = "admin",
        //    };
        //    IdentityRole user = new()
        //    {
        //        Name = "user",
        //    };

        //    await _roleManager.CreateAsync(admin);
        //    await _roleManager.CreateAsync(user);
        //    return Ok();
        //}

    }
}
