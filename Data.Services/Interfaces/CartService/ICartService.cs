using Data.Interface.DataModels.Carts;
using Data.Interface.DataModels.Products;
using Microsoft.AspNetCore.Http;

namespace Data.Services.Interfaces.CartService
{
    public interface ICartService
    {
        Task<CartData> AddItem(int productId, string cartIdStr);
        Task<CartData> RemoveItem(int productId, string cartIdStr);
        Task<CartData> CreateCart();
        Task ClearCart(string cartIdStr);
        Task<CartData> GetCartById(string cartIdStr);
    }
}
