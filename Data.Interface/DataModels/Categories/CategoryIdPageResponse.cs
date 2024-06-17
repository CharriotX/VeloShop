using Data.Interface.DataModels.Products;
using Data.Interface.DataModels.Subcategories;

namespace Data.Interface.DataModels.Categories
{
    public class CategoryIdPageResponse
    {
        public CategoryData Category { get; set; }
        public List<ProductData> Products { get; set; }
        public List<SubcategoryData> Subcategories { get; set; }
    }
}
