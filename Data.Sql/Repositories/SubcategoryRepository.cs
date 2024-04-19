using Data.Interface.DataModels.Subcategories;
using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class SubcategoryRepository : BaseRepository<Subcategory>, ISubcategoryRepository
    {
        private ICategoryRepository _categoryRepository;
        public SubcategoryRepository(WebContext webContext, ICategoryRepository categoryRepository) : base(webContext)
        {
            _categoryRepository = categoryRepository;
        }

        public void AddSubCategoryToCategory(string categoryName, string subCategoryName)
        {
            var category = _categoryRepository.GetByName(categoryName);
            var model = new Subcategory()
            {
                Name = subCategoryName,
                Category = category,
                IsActive = true
            };

            Add(model);
        }

        public SubcategoryData GetSubcategoryData(int id)
        {
            var subcategory = Get(id);
            var data = new SubcategoryData
            {
                Id = subcategory.Id,
                Name = subcategory.Name
            };

            return data;
        }
    }
}
