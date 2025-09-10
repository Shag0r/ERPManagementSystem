using ERPsystemAPP.BusinessModels.TableModels.MenuCategory;

namespace ERPsystemAPP.BusinessModels.Models.MenuCategoryModels
{
    public class MenuCategoriesResponseDto : MenuCategories
    {
        public List<MenuCategoriesResponseDto> DataList { get; set; } = new List<MenuCategoriesResponseDto>();
    }
}
