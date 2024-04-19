using Data.Interface.DataModels.Categories;

namespace Data.Services.Interfaces.CategoriesService
{
    public interface ICategoryService
    {
        List<CategoryData> GetAllCategories();
        List<CategoryWithSubcategoriesData> GetAllCategoriesWithSubcategories();
        Task<CategoryWithSubcategoriesData> GetCategoryById(int id);
    }
}
