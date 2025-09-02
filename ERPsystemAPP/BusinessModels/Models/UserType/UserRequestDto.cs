namespace ERPsystemAPP.BusinessModels.Models.UserType
{
    public class UserRequestDto
    {
        public int? Id { get; set; }
        public int Type { get; set; } = 0;
        public string Name { get; set; }
        public string UserId { get; set; }=string.Empty;
        public string Remarks { get; set; }=string.Empty;
    }
}
