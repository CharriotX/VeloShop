using Data.Interface.DataModels.PaginationData;
using Data.Interface.DataModels.Products;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<ProductData> GetProductData(int id);
        Task<List<ProductData>> GetAllProductsByCategory(int categoryId);
        Task<List<ProductData>> GetProductsBySubcategory(int subcategoryId);

        Task<PageResponse<ProductData>> GetProductsByCategoryWithPagination(int categoryId, int pageNumber, int pageSize);
        Task<PageResponse<ProductData>> GetProductsBySubcategoryWithPagination(int subcategoryId, int pageNumber, int pageSize);
    }
}
