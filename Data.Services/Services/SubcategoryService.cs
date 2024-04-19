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

        public void AddNewSubcategory(NewSubcategoryData data)
        {
            _subcategoryRepository.AddSubCategoryToCategory(data.CategoryName, data.SubcategoryName);
        }

        public void RemoveSubcategory(int id)
        {
            _subcategoryRepository.Remove(id);
        }

        public SubcategoryData GetSubcategory(int id)
        {
            return _subcategoryRepository.GetSubcategoryData(id);
        }
    }
}
