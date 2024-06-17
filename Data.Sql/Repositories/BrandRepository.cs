using Data.Interface.DataModels.Brands;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        private ICategoryRepository _categoryRepository;
        public BrandRepository(WebContext webContext, ICategoryRepository categoryRepository) : base(webContext)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<BrandData> GetBrandByName(string name)
        {
            var brand = await _dbSet.Include(x => x.Category).Where(x => x.Name == name).FirstOrDefaultAsync();
            return new BrandData
            {
                Id = brand.Id,
                Name = brand.Name,
                CategoryId = brand.Category.Id
            };
        }

        public async Task<List<BrandData>> GetAllBrandData()
        {
            var brands = await _dbSet.Include(x => x.Category).ToListAsync();
            return brands.Select(x => new BrandData
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.Category.Id
            }).ToList();
        }

        public async Task<List<BrandData>> GetBrandsByCategoryId(int categoryId)
        {
            var brands = await _dbSet.Include(x => x.Category).Where(x => x.Category.Id == categoryId).ToListAsync();

            return brands.Select(x => new BrandData
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.Category.Id
            }).ToList();
        }

        public async Task<BrandData> CreateBrand(BrandData data)
        {
            var category = await _categoryRepository.Get(data.CategoryId);

            var model = new Brand()
            {
                IsActive = true,
                Name = data.Name,
                Category = category,
            };

            await Add(model);
            return await GetBrandByName(model.Name);
        }

        public async Task<BrandData> UpdateBrand(BrandData data)
        {
            var category = await _categoryRepository.Get(data.CategoryId);

            var model = new Brand()
            {
                IsActive = true,
                Name = data.Name,
                Category = category,
            };

            await Add(model);
            return await GetBrandByName(model.Name);
        }
    }
}
