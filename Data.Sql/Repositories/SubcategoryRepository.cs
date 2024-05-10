using Data.Interface.DataModels.Products;
using Data.Interface.DataModels.Subcategories;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class SubcategoryRepository : BaseRepository<Subcategory>, ISubcategoryRepository
    {
        private ICategoryRepository _categoryRepository;
        public SubcategoryRepository(WebContext webContext, ICategoryRepository categoryRepository) : base(webContext)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddSubCategoryToCategory(string categoryName, string subCategoryName)
        {
            var category = await _categoryRepository.GetByName(categoryName);
            var model = new Subcategory()
            {
                Name = subCategoryName,
                Category = category,
                IsActive = true
            };

            await Add(model);
        }

        public async Task<SubcategoryData> GetSubcategoryData(int id)
        {
            var subcategory = await Get(id);
            var data = new SubcategoryData
            {
                Id = subcategory.Id,
                Name = subcategory.Name
            };

            return data;
        }
    }
}
