using Dapper;
using ERPsystemAPP.BusinessLogicInterfaces;
using ERPsystemAPP.BusinessModels.Models.UserType;
using ERPsystemAPP.DataAccessLayer;
using System.Data;

namespace ERPsystemAPP.BusinessLogicImplementation
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly IDataAccessHelper _dataAccessHelper;
        public UserTypeRepository(IDataAccessHelper dataAccessHelper)
        {
            _dataAccessHelper = dataAccessHelper;
        }

        public async Task<int> UserTypeInsert(UserRequestDto model)
        {
            try
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("Type", model.Type);
                p.Add("Name", model.Name);
                p.Add("Remarks",model.Remarks);
                p.Add("UserId", model.UserId);
               
                p.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _dataAccessHelper.ExecuteData("USP_UserType_Insert", p);
                return p.Get<int>("Id");
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert category: " + ex.Message);
            }
        }
    }
}
