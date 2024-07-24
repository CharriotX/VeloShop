using Data.Interface.DataModels.Helpers;
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

        [HttpGet]
        public async Task<ActionResult> GetAll(string? searchTerm, string? searchColumn = "id", string? sortOrder = "asc", int page = 1, int pageSize = 10)
        {
            var query = new ProductQueryObject
            {
                SearchTerm = searchTerm,
                SortColumn = searchColumn,
                SortOrder = sortOrder,
                PageNumber = page,
                PageSize = pageSize
            };

            var pageResponse = await _productService.GetAll(query);
            return Ok(pageResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var data = await _productService.GetProductData(id);
            return Ok(data);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult> GetAllProductsByCategoryWithPagination(int categoryId, string? searchTerm, string searchColumn, string? sortOrder = "asc", int page = 1, int pageSize = 5)
        {
            var query = new ProductQueryObject
            {
                SearchTerm = searchTerm,
                SortColumn = searchColumn,
                SortOrder = sortOrder,
                PageNumber = page,
                PageSize = pageSize
            };

            var products = await _productService.GetProductDataByCategory(categoryId, query);
            return Ok(products);
        }

        [HttpGet("subcategory/{subcategoryId}")]
        public async Task<ActionResult> GetAllProductsBySubcategory(int subcategoryId, string? searchTerm, string searchColumn, string? sortOrder = "asc", int page = 1, int pageSize = 5)
        {
            var query = new ProductQueryObject
            {
                SearchTerm = searchTerm,
                SortColumn = searchColumn,
                SortOrder = sortOrder,
                PageNumber = page,
                PageSize = pageSize
            };

            var products = await _productService.GetProductDataBySubcategory(subcategoryId, query);
            return Ok(products);
        }

        [HttpGet("bikeCategory")]
        public async Task<ActionResult> GetProductsByBikeCategory(string? searchTerm, string searchColumn, string? searchBrand, string? sortOrder = "asc", int page = 1, int pageSize = 5)
        {
            var query = new ProductQueryObject
            {
                SearchTerm = searchTerm,
                SearchBrand = searchBrand,
                SortColumn = searchColumn,
                SortOrder = sortOrder,
                PageNumber = page,
                PageSize = pageSize
            };

            var products = await _productService.GetByBikeGategory(query);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductData data)
        {
            var product = await _productService.CreateProduct(data);
            return Ok(product);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateProduct([FromBody] CreateProductData data)
        {
            await _productService.UpdateProduct(data);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
