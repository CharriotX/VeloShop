using Data.Interface.DataModels.Categories;
using Data.Interface.DataModels.PaginationData;
using Data.Interface.DataModels.Products;
using Data.Interface.DataModels.Specifications;
using Data.Interface.DataModels.Subcategories;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Data.Services.Interfaces.ProductsService;

namespace Data.Services.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductData> GetProductData(int id)
        {
            var data = await _productRepository.GetProductData(id);
            return data;
        }

        public async Task<List<ProductData>> GetProductDataByCategory(int categoryId)
        {
            var data = await _productRepository.GetAllProductsByCategory(categoryId);
            return data;
        }

        public async Task<List<ProductData>> GetProductDataBySubcategory(int subcategoryId)
        {
            var data = await _productRepository.GetProductsBySubcategory(subcategoryId);
            return data;
        }

        public async Task<PageResponse<ProductData>> GetProductDataByCategoryWithPagination(int categoryId, int pageNumber, int pageSize)
        {
            var data = await _productRepository.GetProductsByCategoryWithPagination(categoryId, pageNumber, pageSize);
            return data;
        }

        public async Task<PageResponse<ProductData>> GetProductDataBySubcategoryWithPagination(int subcategoryId, int pageNumber, int pageSize)
        {
            var data = await _productRepository.GetProductsBySubcategoryWithPagination(subcategoryId, pageNumber, pageSize);
            return data;
        }


    }
}
