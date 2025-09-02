using ERPsystemAPP.BusinessModels.TableModels.UserTypeModel;

namespace ERPsystemAPP.BusinessModels.Models.UserType
{
    public class UserTypeResponseDto : UserTypeModel
    {
        public List<UserTypeResponseDto> Datalist { get; set; } = new List<UserTypeResponseDto>();
    }
}
