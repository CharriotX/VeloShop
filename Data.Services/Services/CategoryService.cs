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

        public async Task<List<CategoryData>> GetAllCategories()
        {
            var models = await _categoryRepository.GetAll();
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
            
            return data;
        }

        public async Task<CategoryWithSubcategoriesData> GetCategoryById(int id)
        {
            var data = await _categoryRepository.GetCategoryDataById(id);            

            return data;
        }

        public async Task<CategoryData> GetCategoryByName(string name)
        {
            var data = await _categoryRepository.GetCategoryDataByName(name);

            return data;
        }

        public async Task<CategoryDataForAddProduct> GetCategoryDataForAddProduct(int id)
        {
            var data = await _categoryRepository.GetCategoryDataForAddProduct(id);

            return data;
        }
    }
}
