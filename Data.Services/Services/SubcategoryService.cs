using Data.Interface.DataModels.Subcategories;
using Data.Interface.Repositories;
using Data.Services.Interfaces.SubcategoriesService;

namespace Data.Services.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private ISubcategoryRepository _subcategoryRepository;
        public SubcategoryService(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task AddNewSubcategory(NewSubcategoryData data)
        {
            await _subcategoryRepository.AddSubCategoryToCategory(data.CategoryName, data.SubcategoryName);
        }

        public async Task RemoveSubcategory(int id)
        {
            await _subcategoryRepository.Remove(id);
        }

        public async Task<SubcategoryData> GetSubcategory(int id)
        {
            return await _subcategoryRepository.GetSubcategoryData(id);
        }
    }
}
