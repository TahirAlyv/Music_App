using IdentityService.Dtos;
using IdentityService.Models;
using IdentityService.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            string defaultRole = "User";
            var result= await _accountService.LoginAsync(model, defaultRole!);

            if (string.IsNullOrEmpty(result))
                return Unauthorized("Invalid credentials");


            return Ok(result);

        }


        [HttpPost("register")]


        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            string defaultRole = "User";
            var (isSuccess, errors) = await _accountService.RegisterAsync(model, defaultRole);

            if (!isSuccess)
                return BadRequest(new { message = "Registration failed", errors });

            return Ok(new { message = "Registration successful", info = model });
        }



    }
}
