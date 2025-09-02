using ERPsystemAPP.BusinessModels.Models.UserType;

namespace ERPsystemAPP.BusinessLogicInterfaces
{
    public interface IUserTypeRepository
    {
        Task<int> UserTypeInsert (UserRequestDto model);
    }
}
