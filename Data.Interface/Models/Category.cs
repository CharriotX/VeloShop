namespace Data.Interface.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public virtual List<Brand> Brands { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Subcategory> Subcategories { get; set; }
        public virtual List<Specification> Specifications { get; set; }
    }
}
