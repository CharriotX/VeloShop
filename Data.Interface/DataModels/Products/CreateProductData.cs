using Data.Interface.DataModels.Specifications;

namespace Data.Interface.DataModels.Products
{
    public class CreateProductData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int  SubcategoryId { get; set; }
        public List<ProductSpecificationData> ProductSpecifications { get; set; } = [];
    }
}
