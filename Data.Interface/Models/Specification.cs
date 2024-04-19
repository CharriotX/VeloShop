namespace Data.Interface.Models
{
    public class Specification : BaseModel
    {
        public string Name { get; set; }
        
        public virtual Category Category { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
