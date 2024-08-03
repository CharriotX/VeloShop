using Data.Interface.DataModels.Products;

namespace Data.Interface.DataModels.Carts
{
    public class CartItemData
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
    }
}
