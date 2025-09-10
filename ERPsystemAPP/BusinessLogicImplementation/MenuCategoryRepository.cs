using Dapper;
using ERPsystemAPP.BusinessLogicInterfaces;
using ERPsystemAPP.BusinessModels;
using ERPsystemAPP.BusinessModels.Models.MenuCategoryModels;
using ERPsystemAPP.DataAccessLayer;
using System.Data;

namespace ERPsystemAPP.BusinessLogicImplementation
{
    public class MenuCategoryRepository : IMenuCategoryRepository
    {
        private readonly IDataAccessHelper _dataAccessHelper;
        public MenuCategoryRepository(IDataAccessHelper dataAccessHelper)
        {
            _dataAccessHelper = dataAccessHelper;
        }
        public Task<int> DeleteMenuCategories(int MenuCategoriesId, MenuCategoryRequestDto model)
        {
            throw new NotImplementedException();
        }

        public Task<List<MenuCategoriesResponseDto>> GetDistinctMenuCategories(MenuCategoriesFilterDto model)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedListModel<MenuCategoriesResponseDto>> GetMenuCategories(int pageNumber, MenuCategoriesFilterDto model)
        {
            throw new NotImplementedException();
        }

        public Task<MenuCategoriesResponseDto> GetMenuCategoriesById(int MenuCategoriesId)
        {
            throw new NotImplementedException();
        }

        public Task<MenuCategoriesResponseDto> GetMenuCategoriesByName(MenuCategoryRequestDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertMenuCategories(MenuCategoryRequestDto model)
        {
            DynamicParameters p = new DynamicParameters();

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);
            p.Add("CategoryName", model.CategoryName);
            p.Add("CategoryDescription", model.CategoryDescription);
            p.Add("CategoryCode", model.CategoryCode);
          
            p.Add("UserId", model.UserId);

            await _dataAccessHelper.ExecuteData("USP_MenuCategories_Insert", p);
            return p.Get<int>("Id");
        }

        public Task<int> UpdateMenuCategories(int MenuCategoriesId, MenuCategoryRequestDto model)
        {
            throw new NotImplementedException();
        }
    }
}
