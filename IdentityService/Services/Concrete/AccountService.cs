using IdentityService.Data;
using IdentityService.Dtos;
using IdentityService.Models;
using IdentityService.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<ApplicationUser> userManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        public async Task<string> LoginAsync(LoginDto dto, string role)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var token = await _tokenService.GenerateToken(user, role);
                return token;
            }

            return null!;
        }

        public async Task<(bool isSuccess, List<string> errors)> RegisterAsync(RegisterDto dto, string role)
        {
            var newUser = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
                ProfilImage = dto.ProfilImage
            };

            var result = await _userManager.CreateAsync(newUser, dto.Password);

            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToList());

            if (!string.IsNullOrEmpty(role))
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                await _userManager.AddToRoleAsync(newUser, role);
            }

            return (true, new List<string>());
        }

    }
}
