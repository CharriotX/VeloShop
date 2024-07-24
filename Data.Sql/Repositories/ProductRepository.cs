using Data.Interface.DataModels.Brands;
using Data.Interface.DataModels.Categories;
using Data.Interface.DataModels.Helpers;
using Data.Interface.DataModels.PaginationData;
using Data.Interface.DataModels.Products;
using Data.Interface.DataModels.Specifications;
using Data.Interface.DataModels.Subcategories;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Data.Sql.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private ICategoryRepository _categoryRepository;
        private ISpecificationRepository _specificationRepository;
        private IBrandRepository _brandRepository;
        private IProductSpecificationRepository _productSpecificationRepository;
        private ISubcategoryRepository _subcategoryRepository;
        public ProductRepository(WebContext webContext,
            ICategoryRepository categoryRepository,
            ISpecificationRepository specificationRepository,
            IBrandRepository brandRepository,
            IProductSpecificationRepository productSpecificationRepository,
            ISubcategoryRepository subcategoryRepository) : base(webContext)
        {
            _categoryRepository = categoryRepository;
            _specificationRepository = specificationRepository;
            _brandRepository = brandRepository;
            _productSpecificationRepository = productSpecificationRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<PagedList<AdminProductData>> GetAll(ProductQueryObject query)
        {
            IQueryable<Product> productQuery = _dbSet
                .Where(x => x.IsActive)
                .Include(x => x.Subcategory)
                .Include(x => x.Category)
                .Include(x => x.Brand);

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(query.SearchTerm));
            }

            if (query.SortOrder.ToLower() == "desc")
            {
                productQuery = productQuery.OrderByDescending(GetSortedProperty(query));
            }
            else
            {
                productQuery = productQuery.OrderBy(GetSortedProperty(query));
            }

            var productsData = await productQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(x => new AdminProductData
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    BrandName = x.Brand.Name,
                    SubcategoryName = x.Subcategory.Name
                }).ToListAsync();

            return new PagedList<AdminProductData>
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalRecords = productQuery.Count(),
                TotalPages = (int)Math.Ceiling((decimal)productQuery.Count() / (decimal)query.PageSize),
                Items = productsData
            };
        }

        public async Task<PagedList<ProductData>> GetByBikeCategory(ProductQueryObject query)
        {
            var bikeCategory = await _categoryRepository.GetByName("Велосипеды");

            IQueryable<Product> productQuery = _dbSet
                .Where(x => x.IsActive)
                .Include(x => x.Subcategory)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Where(x => x.Category == bikeCategory);


            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(query.SearchTerm));
            }
            if (!string.IsNullOrWhiteSpace(query.SearchBrand))
            {
                productQuery = productQuery.Where(p => p.Brand.Name.Contains(query.SearchBrand));
            }

            if (query.SortOrder.ToLower() == "desc")
            {
                productQuery = productQuery.OrderByDescending(GetSortedProperty(query));
            }
            else
            {
                productQuery = productQuery.OrderBy(GetSortedProperty(query));
            }

            var productsData = await productQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(x => new ProductData
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Brand = new BrandData
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name,
                    },
                    Category = new CategoryData
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name,
                    },
                    Subcategory = new SubcategoryData
                    {
                        Id = x.Subcategory.Id,
                        Name = x.Subcategory.Name,
                    }
                }).ToListAsync();

            return new PagedList<ProductData>
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalRecords = productQuery.Count(),
                TotalPages = (int)Math.Ceiling((decimal)productQuery.Count() / (decimal)query.PageSize),
                Items = productsData
            };
        }

        public async Task<ProductData> GetProductData(int id)
        {
            var product = await _dbSet
                .Include(x => x.Category)
                .ThenInclude(x => x.Subcategories)
                .Include(x => x.Specifications)
                .Include(x => x.Brand)
                .Include(x => x.ProductSpecifications)
                .FirstOrDefaultAsync(x => x.Id == id);

            return new ProductData
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Brand = new BrandData
                {
                    Id = product.Brand.Id,
                    Name = product.Brand.Name
                },
                Category = new CategoryData
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                },
                Subcategory = new SubcategoryData
                {
                    Id = product.Subcategory.Id,
                    Name = product.Subcategory.Name,
                },
                ProductSpecifications = product.ProductSpecifications.Select(x => new ProductSpecificationData
                {
                    Name = _specificationRepository.Get(x.SpecificationId).Result.Name,
                    Value = x.Value
                }).ToList()
            };
        }

        public async Task<List<ProductData>> GetAllProductsByCategory(int categoryId)
        {
            var category = await _categoryRepository.Get(categoryId);
            var products = await _dbSet
                .Include(x => x.Category)
                .ThenInclude(x => x.Subcategories)
                .Include(x => x.Specifications)
                .Include(x => x.Brand)
                .Where(x => x.Category == category)
                .ToListAsync();

            var productsData = products.Select(x => new ProductData
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Brand = new BrandData
                {
                    Id = x.Brand.Id,
                    Name = x.Brand.Name
                },
                Category = new CategoryData
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name,
                },
                Subcategory = new SubcategoryData
                {
                    Id = x.Subcategory.Id,
                    Name = x.Subcategory.Name,
                }
            }).ToList();

            return productsData;
        }

        public async Task<PageResponse<CategoryIdPageResponse>> GetProductsByCategory(int categoryId, ProductQueryObject queryObject)
        {
            var totalRecords = await _dbSet.Where(x => x.Category.Id == categoryId).CountAsync();

            IQueryable<Product> productQuery = _dbSet
                .Where(x => x.IsActive)
                .Include(x => x.Subcategory)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Where(x => x.Category.Id == categoryId);

            var filteredAndSortedProducts = GetFilteredProperty(productQuery, queryObject)
                .Skip((queryObject.PageNumber - 1) * queryObject.PageSize)
                .Take(queryObject.PageSize);

            var categoryWithSubcategories = await _categoryRepository.GetAllSubcategoriesOfTheCategory(categoryId);

            var data = new CategoryIdPageResponse
            {
                Products = filteredAndSortedProducts.Select(x => new ProductData
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Brand = new BrandData
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name
                    },
                    Category = new CategoryData
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name,
                    },
                    Subcategory = new SubcategoryData
                    {
                        Id = x.Subcategory.Id,
                        Name = x.Subcategory.Name,
                    }
                }).ToList(),
                Category = new CategoryData
                {
                    Id = categoryWithSubcategories.Id,
                    Name = categoryWithSubcategories.Name,
                },
                Subcategories = categoryWithSubcategories.Subcategories.Select(x => new SubcategoryData
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList(),
            };

            return new PageResponse<CategoryIdPageResponse>
            {
                PageNumber = queryObject.PageNumber,
                PageSize = queryObject.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)queryObject.PageSize),
                Data = data
            };
        }

        public async Task<PageResponse<SubcategoryIdPagePesponse>> GetProductsBySubcategory(int subcategoryId, ProductQueryObject queryObject)
        {
            var totalRecords = await _dbSet.AsNoTracking().Where(x => x.Subcategory.Id == subcategoryId).CountAsync();
            IQueryable<Product> productQuery = _dbSet
                 .Where(x => x.IsActive)
                 .Include(x => x.Subcategory)
                 .Include(x => x.Category)
                 .Include(x => x.Brand)
                 .Where(x => x.Subcategory.Id == subcategoryId);

            var filteredAndSortedProducts = GetFilteredProperty(productQuery, queryObject)
                .Skip((queryObject.PageNumber - 1) * queryObject.PageSize)
                .Take(queryObject.PageSize);

            var subcategoryData = await _subcategoryRepository.GetSubcategoryData(subcategoryId);

            var data = new SubcategoryIdPagePesponse
            {
                Products = filteredAndSortedProducts.Select(x => new ProductData
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Brand = new BrandData
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name
                    },
                    Category = new CategoryData
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name,
                    },
                    Subcategory = new SubcategoryData
                    {
                        Id = x.Subcategory.Id,
                        Name = x.Subcategory.Name,
                    }
                }).ToList(),
                Subcategory = new SubcategoryData
                {
                    Id = subcategoryData.Id,
                    Name = subcategoryData.Name,
                }
            };

            return new PageResponse<SubcategoryIdPagePesponse>
            {
                PageNumber = queryObject.PageNumber,
                PageSize = queryObject.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)queryObject.PageSize),
                Data = data
            };

        }

        public async Task<ProductData> CreateProduct(CreateProductData data)
        {
            var model = new Product()
            {
                Name = data.Name,
                Price = data.Price,
                Description = data.Description,
                Brand = await _brandRepository.Get(data.BrandId),
                Category = await _categoryRepository.Get(data.CategoryId),
                Subcategory = await _subcategoryRepository.Get(data.SubcategoryId),
                IsActive = true
            };

            await Add(model);

            if (data.ProductSpecifications.Count == 0)
            {
                await _productSpecificationRepository.AddSpecificationsToProduct(data.ProductSpecifications, model.Id);
            }

            var createdProduct = await GetProductData(model.Id);

            return createdProduct;
        }

        public async Task UpdateProduct(CreateProductData data)
        {
            var product = await _dbSet
                .Where(x => x.Id == data.Id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(x => x.Name, data.Name)
                    .SetProperty(x => x.Price, data.Price)
                    .SetProperty(x => x.Description, data.Description));
        }

        public async Task UpdateProductBrand(int productId, int brandId)
        {
            var brand = await _brandRepository.Get(brandId);

            var product = await _dbSet
                .Include(x => x.Brand)
                .FirstOrDefaultAsync(x => x.Id == productId);

            product.Brand = brand;
            await _webContext.SaveChangesAsync();
        }
        public async Task UpdateProductCategory(int productId, int categoryId, int subcategoryId)
        {
            var subcategory = await _subcategoryRepository.Get(subcategoryId);
            var category = await _categoryRepository.Get(categoryId);

            var product = await _dbSet
                .Include(x => x.Category)
                .Include(x => x.Subcategory)
                .FirstOrDefaultAsync(x => x.Id == productId);

            product.Category = category;
            product.Subcategory = subcategory;

            await _webContext.SaveChangesAsync();
        }
        public async Task UpdateProductSpecifications(int productId, List<ProductSpecificationData> specifications)
        {
            var product = await _dbSet
                .Include(x => x.ProductSpecifications)
                .FirstOrDefaultAsync(x => x.Id == productId);

            for (int i = 0; i < product.ProductSpecifications.Count; i++)
            {
                if (product.ProductSpecifications[i].Value != specifications[i].Value)
                {
                    product.ProductSpecifications[i].Value = specifications[i].Value;
                }
            }

            await _webContext.SaveChangesAsync();
        }
        public async Task DeleteProduct(int id)
        {
            var product = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            product.IsActive = false;
            _webContext.SaveChanges();
        }

        private static Expression<Func<Product, object>> GetSortedProperty(ProductQueryObject queryObject)
        {
            return queryObject.SortColumn?.ToLower() switch
            {
                "brand" => product => product.Brand.Name,
                "category" => product => product.Category.Name,
                "price" => product => product.Price,
                "name" => product => product.Name,
                _ => product => product.Id,
            };
        }

        private IQueryable<Product> GetFilteredProperty(IQueryable<Product> productQuery, ProductQueryObject queryObject)
        {
            if (!string.IsNullOrWhiteSpace(queryObject.SearchTerm))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(queryObject.SearchTerm));
            }
            if (!string.IsNullOrWhiteSpace(queryObject.SearchBrand))
            {
                productQuery = productQuery.Where(p => p.Brand.Name.Contains(queryObject.SearchBrand));
            }

            if (queryObject.SortOrder.ToLower() == "desc")
            {
                productQuery = productQuery.OrderByDescending(GetSortedProperty(queryObject));
            }
            else
            {
                productQuery = productQuery.OrderBy(GetSortedProperty(queryObject));
            }

            return productQuery;
        }
    }
}
