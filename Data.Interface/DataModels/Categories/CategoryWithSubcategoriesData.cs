using Data.Interface.DataModels.Subcategories;

namespace Data.Interface.DataModels.Categories
{
    public class CategoryWithSubcategoriesData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubcategoryData> Subcategories { get; set; }
        public int TotalProducts { get; set; }  
    }
}
