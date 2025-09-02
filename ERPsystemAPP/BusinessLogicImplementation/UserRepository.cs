using ERPsystemAPP.BusinessLogicInterfaces;
using ERPsystemAPP.BusinessModels.Models.UserBusinessModels;
using ERPsystemAPP.BusinessModels.TableModels.User;
using ERPsystemAPP.DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ERPsystemAPP.BusinessLogicImplementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;  // For refresh tokens

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                              IConfiguration configuration, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _db  = db;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterRequestDto registerDto)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    UserType = registerDto.UserType ?? 0
                };
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    // Optionally assign roles
                }
                return result;
            }
            catch (Exception ex)
            {
                // Log ex
                throw new Exception("Registration failed: " + ex.Message);
            }
        }

        public async Task<(string AccessToken, string RefreshToken,string userId)> LoginUserAsync(LoginRequestDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null) throw new Exception("Invalid credentials");

                var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
                if (!result.Succeeded) throw new Exception("Invalid credentials");

                var accessToken = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"));
                await _userManager.UpdateAsync(user);

                return (accessToken, refreshToken, user.Id);
            }
            catch (Exception ex)
            {
                // Log ex
                throw new Exception("Login failed: " + ex.Message);
            }
        }

        public async Task<string> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.UtcNow);
                if (user == null) throw new Exception("Invalid refresh token");

                var newAccessToken = GenerateJwtToken(user);
                var newRefreshToken = GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"));
                await _db.SaveChangesAsync();

                return newAccessToken;  // Or return both if needed
            }
            catch (Exception ex)
            {
                // Log ex
                throw new Exception("Token refresh failed: " + ex.Message);
            }
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("UserType", user.UserType.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.GetValue<int>("AccessTokenExpirationMinutes")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
