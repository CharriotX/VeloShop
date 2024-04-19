﻿using Data.Interface.DataModels.Subcategories;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface ISubcategoryRepository : IBaseRepository<Subcategory>
    {
        void AddSubCategoryToCategory(string categoryName, string subCategoryName);
        SubcategoryData GetSubcategoryData(int id);
    }
}
