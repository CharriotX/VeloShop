using Data.Interface.DataModels.Carts;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<CartData> CreateCart();
        Task<CartData> GetCartById(int id);
        Task<CartData> AddItemToCart(int cartId, int productId);
        Task<CartData> RemoveItem(int cartId, int productId);
        Task ClearCart(int cartId);
    }
}
