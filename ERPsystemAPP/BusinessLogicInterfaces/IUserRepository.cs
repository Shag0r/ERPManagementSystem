using ERPsystemAPP.BusinessModels.Models.UserBusinessModels;
using Microsoft.AspNetCore.Identity;

namespace ERPsystemAPP.BusinessLogicInterfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUserAsync(RegisterRequestDto registerDto);
        Task<(string AccessToken, string RefreshToken, string userId)> LoginUserAsync(LoginRequestDto loginDto);
        Task<string> RefreshTokenAsync(string refreshToken);
    }
}
