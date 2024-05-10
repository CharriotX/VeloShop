using Data.Interface.DataModels.Categories;
using Data.Interface.DataModels.Specifications;
using Data.Interface.DataModels.Subcategories;

namespace Data.Interface.DataModels.Products
{
    public class ProductData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName {  get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public CategoryData Category { get; set; }
        public SubcategoryData Subcategory { get; set; }
        public List<ProductSpecificationData> ProductSpecifications { get; set; }
    }
}
