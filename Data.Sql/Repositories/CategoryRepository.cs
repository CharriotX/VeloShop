using System;
using Data.Interface.DataModels.Brands;
using Data.Interface.DataModels.Categories;
using Data.Interface.DataModels.Specifications;
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

        public async Task<Category> GetByName(string name)
        {
            return await _dbSet
                .Include(x => x.Subcategories)
                .Where(x => x.IsActive)
                .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<CategoryData> GetCategoryDataByName(string name)
        {
            var category = await _dbSet.Include(x => x.Subcategories).Where(x => x.IsActive).FirstOrDefaultAsync(x => x.Name == name);

            return new CategoryData
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryWithSubcategoriesData> GetCategoryDataById(int id)
        {
            var category = await _dbSet.Include(x => x.Subcategories).Include(x => x.Specifications).Where(x => x.IsActive).FirstOrDefaultAsync(x => x.Id == id);

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

        public async Task<CategoryDataForAddProduct> GetCategoryDataForAddProduct(int id)
        {
            var category = await _dbSet
                .Where(x => x.IsActive)
                .Include(x => x.Subcategories.Where(x => x.IsActive))
                .Include(x => x.Brands)
                .Include(x => x.Specifications)
                .FirstOrDefaultAsync(x => x.Id == id);

            var data = new CategoryDataForAddProduct
            {
                Id = category.Id,
                Name = category.Name,
                Subcategories = category.Subcategories.Select(x => new SubcategoryData
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList(),
                Specifications = category.Specifications.Select(x => new SpecificationData
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList(),
                Brands = category.Brands.Select(x => new BrandData
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryId = x.Category.Id
                }).ToList()
            };

            return data;
        }

        public async Task<CategoryWithSubcategoriesData> GetAllSubcategoriesOfTheCategory(int categoryId)
        {
            var category = await _dbSet.Include(x => x.Subcategories).FirstOrDefaultAsync(x => x.Id == categoryId);

            var data = new CategoryWithSubcategoriesData
            {
                Id = category.Id,
                Name = category.Name,
                Subcategories = category.Subcategories.Select(x => new SubcategoryData
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList()
            };

            return data;
        }

    }
}
