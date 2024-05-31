using Data.Interface.DataModels.Categories;
using Data.Interface.DataModels.PaginationData;
using Data.Interface.DataModels.Products;
using Data.Interface.DataModels.Subcategories;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<ProductData> GetProductData(int id);
        Task<List<ProductData>> GetAllProductsByCategory(int categoryId);
        Task<List<ProductData>> GetProductsBySubcategory(int subcategoryId);
        Task<ProductData> CreateProduct(CreateProductData data);
        Task<PageResponse<CategoryIdPageResponse>> GetProductsByCategoryWithPagination(int categoryId, int pageNumber, int pageSize);
        Task<PageResponse<SubcategoryIdPagePesponse>> GetProductsBySubcategoryWithPagination(int subcategoryId, int pageNumber, int pageSize);
    }
}
