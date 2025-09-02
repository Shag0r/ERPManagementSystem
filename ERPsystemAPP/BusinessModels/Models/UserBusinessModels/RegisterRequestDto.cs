namespace ERPsystemAPP.BusinessModels.Models.UserBusinessModels
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? UserType { get; set; } = 0;
    }
}
