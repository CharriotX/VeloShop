using Data.Interface.DataModels.Categories;
using Data.Interface.DataModels.Helpers;
using Data.Interface.DataModels.PaginationData;
using Data.Interface.DataModels.Products;
using Data.Interface.DataModels.Subcategories;

namespace Data.Services.Interfaces.ProductsService
{
    public interface IProductService
    {
        Task<PagedList<AdminProductData>> GetAll(ProductQueryObject productQuery);
        Task<ProductData> GetProductData(int id);
        Task<PagedList<ProductData>> GetByBikeGategory(ProductQueryObject productQuery);
        Task<ProductData> CreateProduct(CreateProductData data);
        Task DeleteProduct(int id);
        Task UpdateProduct(CreateProductData data);
        Task<PageResponse<CategoryIdPageResponse>> GetProductDataByCategory(int categoryId, ProductQueryObject productQuery);
        Task<PageResponse<SubcategoryIdPagePesponse>> GetProductDataBySubcategory(int subcategoryId, ProductQueryObject queryObject);
    }
}
