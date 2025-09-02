using ERPsystemAPP.BusinessLogicInterfaces;
using ERPsystemAPP.BusinessModels.Models.UserBusinessModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPsystemAPP.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
        {
            try
            {
                var result = await _userRepository.RegisterUserAsync(registerDto);
                if (result.Succeeded) return Ok("User registered successfully");
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                var (accessToken, refreshToken, userId) = await _userRepository.LoginUserAsync(loginDto);
                return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken,UserId= userId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            try
            {
                var newAccessToken = await _userRepository.RefreshTokenAsync(refreshToken);
                return Ok(new { AccessToken = newAccessToken });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
