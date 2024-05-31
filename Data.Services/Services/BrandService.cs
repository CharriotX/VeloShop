using Data.Interface.DataModels.Brands;
using Data.Interface.Repositories;
using Data.Services.Interfaces.BrandsService;

namespace Data.Services.Services
{
    public class BrandService : IBrandService
    {
        private IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<BrandData> GetBrandByName(string name)
        {
            var brand = await _brandRepository.GetBrandByName(name);
            return brand;
        }

        public async Task<List<BrandData>> GetAllBrandData()
        {
            var brands = await _brandRepository.GetAllBrandData();
            return brands;
        }

        public async Task<List<BrandData>> GetBrandsByCategoryId(int categoryId)
        {
            var brands = await _brandRepository.GetBrandsByCategoryId(categoryId);
            return brands;
        }

        public async Task<BrandData> CreateBrand(BrandData data)
        {
            var brand = await _brandRepository.CreateBrand(data);
            return brand;
        }

    }
}
