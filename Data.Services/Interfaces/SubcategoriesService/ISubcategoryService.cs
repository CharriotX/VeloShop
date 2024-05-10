using Data.Interface.DataModels.Subcategories;

namespace Data.Services.Interfaces.SubcategoriesService
{
    public interface ISubcategoryService
    {
        Task AddNewSubcategory(NewSubcategoryData data);
        Task RemoveSubcategory(int id);
        Task<SubcategoryData> GetSubcategory(int id);
    }
}
