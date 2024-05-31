using Data.Interface.DataModels.Brands;
using Data.Interface.DataModels.Categories;
using Data.Interface.DataModels.Specifications;
using Data.Interface.DataModels.Subcategories;

namespace Data.Interface.DataModels.Products
{
    public class CreateProductData
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int  SubcategoryId { get; set; }
        public List<ProductSpecificationData> ProductSpecifications { get; set; } = [];
    }
}
