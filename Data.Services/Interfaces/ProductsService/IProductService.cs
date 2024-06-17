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
        Task<List<ProductData>> GetProductDataByCategory(int categoryId);
        Task<List<ProductData>> GetProductDataBySubcategory(int subcategoryId);
        Task<ProductData> CreateProduct(CreateProductData data);
        Task DeleteProduct(int id);
        Task UpdateProduct(CreateProductData data);
        Task<PageResponse<CategoryIdPageResponse>> GetProductDataByCategoryWithPagination(int categoryId, int pageNumber, int pageSize);
        Task<PageResponse<SubcategoryIdPagePesponse>> GetProductDataBySubcategoryWithPagination(int subcategoryId, int pageNumber, int pageSize);
    }
}
