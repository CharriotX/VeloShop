using Data.Interface.DataModels.Carts;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Data.Services.Interfaces.CartService;
using Data.Services.Interfaces.ProductsService;
using Microsoft.AspNetCore.Http;

namespace Data.Services.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IProductService productService)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _productService = productService;
        }

        public async Task<CartData> AddItem(int productId, string cartIdStr)
        {
            CartData cartData = new CartData();

            if (cartIdStr == null)
            {
                var cart = await _cartRepository.CreateCart();
                var cartItems = await _cartRepository.AddItemToCart(cart.Id, productId);
                cartData = cartItems;
            }
            else
            {
                var cartId = int.Parse(cartIdStr);
                var cartItems = await _cartRepository.AddItemToCart(cartId, productId);
                cartData = cartItems;
            }
            return cartData;
        }

        public async Task<CartData> RemoveItem(int productId, string cartIdStr)
        {
            var cartId = int.Parse(cartIdStr);
            var cartItems = await _cartRepository.RemoveItem(cartId, productId);
            return cartItems;
        }

        public async Task<CartData> CreateCart()
        {
            var cart = await _cartRepository.CreateCart();
            return cart;
        }

        public async Task<CartData> GetCartById(string cartIdStr)
        {
            var cartId = int.Parse(cartIdStr);
            var cart = await _cartRepository.GetCartById(cartId);

            return cart;
        }

        public async Task CleanCart(string cartIdStr)
        {
            var cartId = int.Parse(cartIdStr);
            await _cartRepository.CleanCart(cartId);
        }
    }
}
