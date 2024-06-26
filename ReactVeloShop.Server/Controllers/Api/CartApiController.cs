using Data.Interface.DataModels.Carts;
using Data.Interface.Models;
using Data.Services.Interfaces.CartService;
using Data.Services.Interfaces.ProductsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Web.Helpers;

namespace ReactVeloShop.Server.Controllers.Api
{
    [ApiController]
    [Route("/api/cart")]
    public class CartApiController : ControllerBase
    {
        private ICartService _cartService;
        private IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionId = "CartId";
        private IProductService _productService;

        public CartApiController(ICartService cartService, IHttpContextAccessor httpContextAccessor, IProductService productService)
        {
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cartId = _httpContextAccessor.HttpContext.Session.GetString(CartSessionId);
            if (cartId == null)
            {
                var cart = await _cartService.CreateCart();
                _httpContextAccessor.HttpContext.Session.SetString(CartSessionId, cart.Id.ToString());
                
                return Ok(cart);
            }
            else
            {
                var cart = await _cartService.GetCartById(cartId);
                return Ok(cart);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromQuery]int productId)
        {
            var cartId = _httpContextAccessor.HttpContext.Session.GetString(CartSessionId);
            var cartData = await _cartService.AddItem(productId, cartId);

            _httpContextAccessor.HttpContext.Session.SetString(CartSessionId, cartData.Id.ToString());

            return Ok(cartData);
        }

        [HttpPatch]
        public async Task<IActionResult> RemoveItem([FromQuery]int productId)
        {

            var cartId = _httpContextAccessor.HttpContext.Session.GetString(CartSessionId);
            var cartData = await _cartService.RemoveItem(productId, cartId);

            return Ok(cartData);
        }

        [HttpDelete]
        public async Task<IActionResult> CleanCart()
        {
            var cartId = _httpContextAccessor.HttpContext.Session.GetString(CartSessionId);
            if (cartId != null)
            {
               await _cartService.CleanCart(cartId);
            }

            _httpContextAccessor.HttpContext.Session.Remove(CartSessionId);

            return Ok();
        }
    }
}
