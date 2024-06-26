namespace Data.Interface.DataModels.Carts
{
    public class CartData
    {
        public int Id { get; set; }
        public List<CartItemData> Items { get; set; }
        public decimal Total { get; set; }
    }
}
