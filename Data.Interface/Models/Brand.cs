namespace Data.Interface.Models
{
    public class Brand : BaseModel
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public Category Category { get; set; }
    }
}
