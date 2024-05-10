using Data.Interface.DataModels.Categories;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetByName(string name);
        Task<CategoryData> GetCategoryDataByName(string name);
        Task<CategoryWithSubcategoriesData> GetCategoryDataById(int id);
        Task<List<CategoryWithSubcategoriesData>> GetCategoriesWithSubcategories();
        Task<CategoryWithSubcategoriesData> GetAllSubcategoriesOfTheCategory(int categoryId);
    }
}
