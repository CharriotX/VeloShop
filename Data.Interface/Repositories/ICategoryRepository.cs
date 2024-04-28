using Data.Interface.DataModels.Categories;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Category GetByName(string name);
        CategoryData GetCategoryDataByName(string name);
        Task<CategoryWithSubcategoriesData> GetCategoryDataById(int id);
        Task<List<CategoryWithSubcategoriesData>> GetCategoriesWithSubcategories();
    }
}
