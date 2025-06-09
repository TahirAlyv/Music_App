using IdentityService.Dtos;

namespace IdentityService.Services.Interface
{
    public interface IAccountService
    {

        Task<(bool isSuccess, List<string> errors)> RegisterAsync(RegisterDto dto, string role);
        Task<string> LoginAsync(LoginDto dto, string role);
       
    }
}
