using Microsoft.AspNetCore.Identity;

namespace ERPsystemAPP.BusinessModels.TableModels.User
{
    public class ApplicationUser : IdentityUser
    {
        public int UserType { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
    }
}
