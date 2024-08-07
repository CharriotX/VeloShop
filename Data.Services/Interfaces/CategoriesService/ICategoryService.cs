﻿using Data.Interface.DataModels.Categories;

namespace Data.Services.Interfaces.CategoriesService
{
    public interface ICategoryService
    {
        Task<List<CategoryData>> GetAllCategories();
        Task<List<CategoryWithSubcategoriesData>> GetAllCategoriesWithSubcategories();
        Task<CategoryWithSubcategoriesData> GetCategoryById(int id);
        Task<CategoryData> GetCategoryByName(string name);
        Task<CategoryDataForAddProduct> GetCategoryDataForAddProduct(int id);
    }
}
