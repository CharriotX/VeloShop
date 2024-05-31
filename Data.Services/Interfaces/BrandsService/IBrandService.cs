using Data.Interface.DataModels.Brands;

namespace Data.Services.Interfaces.BrandsService
{
    public interface IBrandService
    {
        Task<BrandData> GetBrandByName(string name);
        Task<List<BrandData>> GetAllBrandData();
        Task<List<BrandData>> GetBrandsByCategoryId(int categoryId);
        Task<BrandData> CreateBrand(BrandData data);
    }
}
