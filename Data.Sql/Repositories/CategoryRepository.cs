using System;
using Data.Interface.DataModels.Categories;
using Data.Interface.DataModels.Subcategories;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(WebContext webContext) : base(webContext)
        {
        }

        public Category GetByName(string name)
        {
            return _dbSet
                .Include(x => x.Subcategories)
                .Where(x => x.IsActive)
                .FirstOrDefault(x => x.Name == name);
        }

        public CategoryData GetCategoryDataByName(string name)
        {
            var category = _dbSet.Include(x => x.Subcategories).Where(x => x.IsActive).FirstOrDefault(x => x.Name == name);

            return new CategoryData
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryWithSubcategoriesData> GetCategoryDataById(int id)
        {
            var category = await _dbSet.Include(x => x.Subcategories).Where(x => x.IsActive).FirstOrDefaultAsync(x => x.Id == id);

            return new CategoryWithSubcategoriesData
            {
                Id = category.Id,
                Name = category.Name,
                Subcategories = category.Subcategories.Select(x => new SubcategoryData
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };
        }

        public async Task<List<CategoryWithSubcategoriesData>> GetCategoriesWithSubcategories()
        {
            var categories = await _dbSet
                .Where(x => x.IsActive)
                .Include(x => x.Subcategories.Where(x => x.IsActive))
                .ToListAsync();

            var data = categories.Select(x => new CategoryWithSubcategoriesData
            {
                Id = x.Id,
                Name = x.Name,
                Subcategories = x.Subcategories.Select(x => new SubcategoryData
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            })
                .ToList();

            return data;
        }
    }
}
