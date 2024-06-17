namespace Data.Interface.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        public virtual List<Specification> Specifications { get; set; }
        public virtual List<ProductSpecification> ProductSpecifications { get; set; }
    }
}
