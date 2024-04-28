using Data.Interface.DataModels.Categories;
using Data.Interface.DataModels.Subcategories;
using Data.Interface.Repositories;
using Data.Services.Interfaces.CategoriesService;

namespace Data.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private ISubcategoryRepository _subcategoryRepository;
        public CategoryService(ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public List<CategoryData> GetAllCategories()
        {
            var models = _categoryRepository.GetAll();
            var data = models.Select(x => new CategoryData
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return data;
        }

        public async Task<List<CategoryWithSubcategoriesData>> GetAllCategoriesWithSubcategories()
        {
            var data = await _categoryRepository.GetCategoriesWithSubcategories();
            var models = data.Select(x => new CategoryWithSubcategoriesData()
            {
                Id = x.Id,
                Name = x.Name,
                Subcategories = x.Subcategories.Select(x => new SubcategoryData
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            }).ToList();

            return models;
        }


        public async Task<CategoryWithSubcategoriesData> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryDataById(id);
            var data = new CategoryWithSubcategoriesData
            {
                Id = category.Id,
                Name = category.Name,
                Subcategories = category.Subcategories.Select(x => new SubcategoryData
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };

            return data;
        }
    }
}
