using IdentityService.Models;

namespace IdentityService.Services.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user,  string role);
    }
}
