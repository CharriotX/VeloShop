using Data.Interface.DataModels.Brands;
using Data.Interface.DataModels.Specifications;
using Data.Interface.DataModels.Subcategories;

namespace Data.Interface.DataModels.Categories
{
    public class CategoryDataForAddProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubcategoryData> Subcategories { get; set; }
        public List<SpecificationData> Specifications { get; set; }
        public List<BrandData> Brands { get; set; }
    }
}
