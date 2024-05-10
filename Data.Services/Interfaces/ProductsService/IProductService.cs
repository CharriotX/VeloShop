using Data.Interface.DataModels.PaginationData;
using Data.Interface.DataModels.Products;

namespace Data.Services.Interfaces.ProductsService
{
    public interface IProductService
    {
        Task<ProductData> GetProductData(int id);
        Task<List<ProductData>> GetProductDataByCategory(int categoryId);
        Task<List<ProductData>> GetProductDataBySubcategory(int subcategoryId);

        Task<PageResponse<ProductData>> GetProductDataByCategoryWithPagination(int categoryId, int pageNumber, int pageSize);
        Task<PageResponse<ProductData>> GetProductDataBySubcategoryWithPagination(int subcategoryId, int pageNumber, int pageSize);
    }
}
