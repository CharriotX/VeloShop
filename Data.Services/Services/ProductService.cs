using Data.Interface.DataModels.Categories;
using Data.Interface.DataModels.Helpers;
using Data.Interface.DataModels.PaginationData;
using Data.Interface.DataModels.Products;
using Data.Interface.DataModels.Subcategories;
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

        public async Task<PagedList<AdminProductData>> GetAll(ProductQueryObject productQuery)
        {
            var products = await _productRepository.GetAll(productQuery);
            return products;
        }

        public async Task<PagedList<ProductData>> GetByBikeGategory(ProductQueryObject productQuery)
        {
            var products = await _productRepository.GetByBikeCategory(productQuery);
            return products;
        }

        public async Task<ProductData> GetProductData(int id)
        {
            var data = await _productRepository.GetProductData(id);
            return data;
        }

        public async Task<PageResponse<CategoryIdPageResponse>> GetProductDataByCategory(int categoryId, ProductQueryObject productQuery)
        {
            var data = await _productRepository.GetProductsByCategory(categoryId, productQuery);

            return data;

        }

        public async Task<PageResponse<SubcategoryIdPagePesponse>> GetProductDataBySubcategory(int subcategoryId, ProductQueryObject queryObject)
        {
            var data = await _productRepository.GetProductsBySubcategory(subcategoryId, queryObject);
            return data;
        }

        public async Task<ProductData> CreateProduct(CreateProductData data)
        {
            var createdProduct = await _productRepository.CreateProduct(data);
            return createdProduct;
        }

        public async Task DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
        }

        public async Task UpdateProduct(CreateProductData data)
        {
            var product = await _productRepository.GetProductData(data.Id);

            if (product.Brand.Id != data.BrandId)
            {
                await _productRepository.UpdateProductBrand(data.Id, data.BrandId);
            }
            if (product.Category.Id != data.CategoryId || product.Subcategory.Id != data.SubcategoryId)
            {
                await _productRepository.UpdateProductCategory(data.Id, data.CategoryId, data.SubcategoryId);
            }
            if (data.ProductSpecifications != null || data.ProductSpecifications.Count != 0)
            {
                await _productRepository.UpdateProductSpecifications(data.Id, data.ProductSpecifications);
            }

            await _productRepository.UpdateProduct(data);
        }
    }
}
