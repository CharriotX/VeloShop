using Data.Interface.DataModels.Subcategories;

namespace Data.Services.Interfaces.SubcategoriesService
{
    public interface ISubcategoryService
    {
        void AddNewSubcategory(NewSubcategoryData data);
        void RemoveSubcategory(int id);
        SubcategoryData GetSubcategory(int id);
    }
}
