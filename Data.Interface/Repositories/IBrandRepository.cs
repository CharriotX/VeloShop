using Data.Interface.DataModels.Brands;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        Task<BrandData> GetBrandByName(string name);
        Task<List<BrandData>> GetAllBrandData();
        Task<List<BrandData>> GetBrandsByCategoryId(int categoryId);
        Task<BrandData> CreateBrand(BrandData data);
    }
}
