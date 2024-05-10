using Data.Interface.DataModels.Subcategories;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface ISubcategoryRepository : IBaseRepository<Subcategory>
    {
        Task AddSubCategoryToCategory(string categoryName, string subCategoryName);
        Task<SubcategoryData> GetSubcategoryData(int id);
    }
}
