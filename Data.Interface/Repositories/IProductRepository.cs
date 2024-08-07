﻿using Data.Interface.DataModels.Categories;
using Data.Interface.DataModels.Helpers;
using Data.Interface.DataModels.PaginationData;
using Data.Interface.DataModels.Products;
using Data.Interface.DataModels.Specifications;
using Data.Interface.DataModels.Subcategories;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<PagedList<AdminProductData>> GetAll(ProductQueryObject query);
        Task<ProductData> GetProductData(int id);
        Task<PagedList<ProductData>> GetByBikeCategory(ProductQueryObject query);
        Task DeleteProduct(int id);
        Task UpdateProductBrand(int productId, int brandId);
        Task UpdateProductCategory(int productId, int categoryId, int subcategoryId);
        Task UpdateProduct(CreateProductData data);
        Task UpdateProductSpecifications(int productId, List<ProductSpecificationData> specifications);
        Task<List<ProductData>> GetAllProductsByCategory(int categoryId);
        Task<ProductData> CreateProduct(CreateProductData data);
        Task<PageResponse<CategoryIdPageResponse>> GetProductsByCategory(int categoryId, ProductQueryObject productQuery);
        Task<PageResponse<SubcategoryIdPagePesponse>> GetProductsBySubcategory(int subcategoryId, ProductQueryObject queryObject);
    }
}
