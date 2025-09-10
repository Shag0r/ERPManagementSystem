using ERPsystemAPP.BusinessModels;
using ERPsystemAPP.BusinessModels.Models.MenuCategoryModels;

namespace ERPsystemAPP.BusinessLogicInterfaces
{
    public interface IMenuCategoryRepository
    {
        Task<PaginatedListModel<MenuCategoriesResponseDto>> GetMenuCategories(int pageNumber, MenuCategoriesFilterDto model);
        Task<List<MenuCategoriesResponseDto>> GetDistinctMenuCategories(MenuCategoriesFilterDto model);
        Task<MenuCategoriesResponseDto> GetMenuCategoriesById(int MenuCategoriesId);
        Task<MenuCategoriesResponseDto> GetMenuCategoriesByName(MenuCategoryRequestDto model);
        Task<int> InsertMenuCategories(MenuCategoryRequestDto model);
        Task<int> UpdateMenuCategories(int MenuCategoriesId, MenuCategoryRequestDto model);
        Task<int> DeleteMenuCategories(int MenuCategoriesId, MenuCategoryRequestDto model);
    }
}
