namespace Data.Interface.Models
{
    public class Cart : BaseModel
    {
        public List<CartItem> CartItems {  get; set; } 
    }
}
