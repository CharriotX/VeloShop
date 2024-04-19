namespace Data.Interface.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Specification> Specifications { get; set; }
    }
}
