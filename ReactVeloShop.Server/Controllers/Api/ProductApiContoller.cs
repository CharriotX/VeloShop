using Data.Interface.DataModels.Products;
using Data.Services.Interfaces.ProductsService;
using Microsoft.AspNetCore.Mvc;

namespace ReactVeloShop.Server.Controllers.Api
{
    [ApiController]
    [Route("/api/product")]
    public class ProductApiContoller : ControllerBase
    {
        private IProductService _productService;
        public ProductApiContoller(IProductService productService)
        {     
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var data = await _productService.GetProductData(id);
            return Ok(data);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult> GetAllProductsByCategoryWithPagination(int categoryId, int pageNumber = 1, int pageSize = 5)
        {
            var products = await _productService.GetProductDataByCategoryWithPagination(categoryId, pageNumber, pageSize);
            return Ok(products);
        }

        [HttpGet("subcategory/{subcategoryId}")]
        public async Task<ActionResult> GetAllProductsBySubcategory(int subcategoryId, int pageNumber = 1, int pageSize = 5)
        {
            var products = await _productService.GetProductDataBySubcategoryWithPagination(subcategoryId, pageNumber, pageSize);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody]CreateProductData data)
        {
            var product = await _productService.CreateProduct(data);
            return Ok(product);
        }
    }
}
