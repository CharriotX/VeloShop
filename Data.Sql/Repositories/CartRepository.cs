using Data.Interface.DataModels.Carts;
using Microsoft.AspNetCore.Http;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Data.Interface.DataModels.Products;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Data.Sql.Repositories
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        private IProductRepository _productRepository;
        public CartRepository(WebContext webContext, IProductRepository productRepository) : base(webContext)
        {
            _productRepository = productRepository;
        }

        public async Task<Cart> GetCart(int id)
        {
            return await _dbSet.Include(x => x.CartItems).ThenInclude(p => p.Product).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<CartData> GetCartById(int id)
        {
            var cart = await GetCart(id);
            var cartData = new CartData
            {
                Id = cart.Id,
                Items = cart.CartItems.Select(x => new CartItemData
                {
                    ProductId = x.Product.Id,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList(),
                ItemsCount = cart.CartItems.Count,
                Total = cart.CartItems.Where(x => x.Cart.Id == cart.Id).Select(x => x.Price).Sum()
            };

            return cartData;
        }

        public async Task<CartData> CreateCart()
        {
            var cart = new Cart();
            cart.IsActive = true;
            await Add(cart);

             var cartData = new CartData
            {
                Id = cart.Id
            };

            return cartData;
        }

        public async Task<CartData> AddItemToCart(int cartId, int productId)
        {
            var product = await _productRepository.GetProductData(productId);
            var cart = await GetCart(cartId);

            var existingCartItem = cart.CartItems.FirstOrDefault(x => x.Product.Id == product.Id);

            if (existingCartItem == null)
            {
                var newCartItem = new CartItem()
                {
                    Cart = cart,
                    Product = await _productRepository.Get(product.Id),
                    Quantity = 1
                };

                _webContext.CartItems.Add(newCartItem);
                await _webContext.SaveChangesAsync();
            }
            else
            {
                existingCartItem.Quantity++;
                _webContext.Update(existingCartItem);
                await _webContext.SaveChangesAsync();
            }

            var cartData = new CartData
            {
                Id = cartId,
                Items = cart.CartItems.Select(x => new CartItemData
                {
                    ProductId = x.Product.Id,
                    ProductName = x.Product.Name,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList(),
                ItemsCount = cart.CartItems.Count,
                Total = cart.CartItems.Where(x => x.Cart.Id == cartId).Select(x=> x.Price).Sum()
            };

            return cartData;
        }

        public async Task<CartData> RemoveItem(int cartId, int productId)
        {
            var cart = await GetCart(cartId);
            var productItem = cart.CartItems.FirstOrDefault(x => x.Product.Id == productId);

            _webContext.CartItems.Remove(productItem);
            await _webContext.SaveChangesAsync();

            var cartData = new CartData
            {
                Id = cartId,
                Items = cart.CartItems.Select(x => new CartItemData
                {
                    ProductId = x.Product.Id,
                    ProductName = x.Product.Name,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList(),
                ItemsCount = cart.CartItems.Count
            };

            return cartData;
        }

        public async Task ClearCart(int cartId)
        {
            var cart = await GetCart(cartId);
            _webContext.Remove(cart);
            await _webContext.SaveChangesAsync();
        }
    }
}
