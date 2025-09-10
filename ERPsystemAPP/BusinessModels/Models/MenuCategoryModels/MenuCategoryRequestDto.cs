namespace ERPsystemAPP.BusinessModels.Models.MenuCategoryModels
{
    public class MenuCategoryRequestDto
    {
        public int? Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryDescription { get; set; }
        public string UserId {  get; set; }=string.Empty;

    }
}
