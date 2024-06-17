using Data.Interface.DataModels.Users;
using Data.Interface.Models;
using Data.Interface.Models.enums;
using Data.Interface.Repositories;
using Data.Services.Interfaces.AuthService;

namespace ReactVeloShop.Server.Utility
{
    public class SeedData
    {
        const string BIKE_CATEGORY_NAME = "Велосипеды";
        const string ACCESSORY_CATEGORY_NAME = "Аксессуары";
        const string SPARE_CATEGORY_NAME = "Запчасти";
        const string TOOL_CATEGORY_NAME = "Инструменты";
        const string EQUIPMENT_CATEGORY_NAME = "Экипировка";
        const string SCOOTER_CATEGORY_NAME = "Самокаты";

        const string ADMIN_NAME_ROLE = "Admin";
        const string ADMIN_EMAIL = "admin@mail.ru";
        const string ADMIN_PASSWORD = "admin";

        const string DEFAULT_BRAND_NAME = "SeedBrand";

        private static List<string> _categories = new List<string> { BIKE_CATEGORY_NAME, ACCESSORY_CATEGORY_NAME, SPARE_CATEGORY_NAME, TOOL_CATEGORY_NAME, EQUIPMENT_CATEGORY_NAME, SCOOTER_CATEGORY_NAME };
        private static List<string> _bikeSubcategories = new List<string> { "Горные велосипеды", "Женские велосипеды", "Детские велосипеды", "Подростковые велосипеды", "Городские велосипеды", "Шоссейные велосипеды", "Складные велосипеды", "Велосипеды BMX", };
        private static List<string> _accessorySubcategories = new List<string> { "Велозамки", "Освещение", "Насосы", "Щитки", "Багажники", "Велокомпьютеры", "Велосумки", "Грипсы", "Подножки" };
        private static List<string> _spareSubcategories = new List<string> { "Велокамеры", "Велопокрышки", "Переключатели", "Педали", "Вилки", "Каретки, системы и шатуны", "Колеса и части", "Манетки и шифтеры", "Рулевое управление", "Подшипники", "Седла и части", "Тормозная система", "Цепи" };
        private static List<string> _toolSubcategories = new List<string> { "Многофункциональные инструменты", "Уход за велосипедом", "Велоаптечки" };
        private static List<string> _equiomentSubcategories = new List<string> { "Шлемы", "Перчатки" };
        private static List<string> _scooterSubcategories = new List<string> { "Взрослые самокаты", "Детские самокаты", "Трюковые самокаты", "Запчасти для самоката" };

        private static List<string> _bikeSpecifications = new List<string> { "Диаметр колес", "Материал рамы", "Размер рамы", "Количество скоростей", "Манетки", "Вилка", "Руль", "Седло" };

        public static async Task Seed(WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateAsyncScope())
            {
                await SeedCategory(scope);
                await SeedSubcategoriesForCategories(scope);
                await SeedSpecifications(scope);
                await SeedUsers(scope);
                await SeedProducts(scope);
                await SeedSpecificationsToBikeCategory(scope);
            }
        }


        public static async Task SeedCategory(IServiceScope scope)
        {
            var categoryRepository = scope.ServiceProvider.GetService<ICategoryRepository>();

            if (!await categoryRepository.Any())
            {
                foreach (var item in _categories)
                {
                    var model = new Category()
                    {
                        Name = item,
                        IsActive = true
                    };

                    await categoryRepository.Add(model);
                }
            }
        }

        public static async Task SeedSubcategoriesForCategories(IServiceScope scope)
        {
            var categoryRepository = scope.ServiceProvider.GetService<ICategoryRepository>();
            var subcategoryRepository = scope.ServiceProvider.GetService<ISubcategoryRepository>();

            if (!await subcategoryRepository.Any())
            {
                var bikeCategory = await categoryRepository.GetByName(BIKE_CATEGORY_NAME);
                foreach (var item in _bikeSubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = bikeCategory,
                        Name = item,
                        IsActive = true
                    };

                    await subcategoryRepository.Add(model);
                }

                var accessoryCategory = categoryRepository.GetByName(ACCESSORY_CATEGORY_NAME);
                foreach (var item in _accessorySubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = accessoryCategory.Result,
                        Name = item,
                        IsActive = true
                    };

                    await subcategoryRepository.Add(model);
                }

                var spareCategory = categoryRepository.GetByName(SPARE_CATEGORY_NAME);
                foreach (var item in _spareSubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = spareCategory.Result,
                        Name = item,
                        IsActive = true
                    };

                    await subcategoryRepository.Add(model);
                }

                var toolCategory = categoryRepository.GetByName(TOOL_CATEGORY_NAME);
                foreach (var item in _toolSubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = toolCategory.Result,
                        Name = item,
                        IsActive = true
                    };

                    await subcategoryRepository.Add(model);
                }

                var equipmentCategory = categoryRepository.GetByName(EQUIPMENT_CATEGORY_NAME);
                foreach (var item in _equiomentSubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = equipmentCategory.Result,
                        Name = item,
                        IsActive = true
                    };

                    await subcategoryRepository.Add(model);
                }

                var scooterCategory = categoryRepository.GetByName(SCOOTER_CATEGORY_NAME);
                foreach (var item in _scooterSubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = scooterCategory.Result,
                        Name = item,
                        IsActive = true
                    };

                    await subcategoryRepository.Add(model);
                }
            }
        }

        public static async Task SeedSpecifications(IServiceScope scope)
        {
            var categoryRepository = scope.ServiceProvider.GetService<ICategoryRepository>();
            var specificationRepository = scope.ServiceProvider.GetService<ISpecificationRepository>();

            if (!await specificationRepository.Any())
            {
                var bikeCategory = await categoryRepository.GetByName(BIKE_CATEGORY_NAME);

                foreach (var item in _bikeSpecifications)
                {
                    var model = new Specification()
                    {
                        Name = item,
                        Category = bikeCategory,
                        IsActive = true
                    };

                    await specificationRepository.Add(model);
                }
            }
        }

        public static async Task SeedUsers(IServiceScope scope)
        {
            var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
            var authService = scope.ServiceProvider.GetService<IAuthService>();

            var adminData = new RegisterUserData
            {
                Email = ADMIN_EMAIL,
                Username = ADMIN_NAME_ROLE,
                Password = ADMIN_PASSWORD
            };

            if (!userRepository.IsUsernameExist(ADMIN_NAME_ROLE).Result)
            {
                await authService.Register(adminData, SiteRole.Admin);
            }
        }

        public static async Task SeedProducts(IServiceScope scope)
        {
            var productRepository = scope.ServiceProvider.GetService<IProductRepository>();
            var brandRepository = scope.ServiceProvider.GetService<IBrandRepository>();
            var categoryRepository = scope.ServiceProvider.GetService<ICategoryRepository>();

            var bikeCategory = await categoryRepository.GetCategoryDataByName(BIKE_CATEGORY_NAME);

            if(!await brandRepository.Any())
            {
                var brand = new Brand()
                {
                    Name = DEFAULT_BRAND_NAME,
                    IsActive=true,
                    Category = await categoryRepository.Get(bikeCategory.Id)
                };

                await brandRepository.Add(brand);
            }

            if (!await productRepository.Any())
            {
                var subcategoryRepository = scope.ServiceProvider.GetService<ISubcategoryRepository>();

                var allCategories = await categoryRepository.GetCategoriesWithSubcategories();
                var defaultBrand = await brandRepository.GetBrandByName(DEFAULT_BRAND_NAME);

                foreach (var category in allCategories)
                {
                    var subcategories = await categoryRepository.GetAllSubcategoriesOfTheCategory(category.Id);
                    foreach (var subcategory in category.Subcategories)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            var productModel = new Product()
                            {
                                Name = subcategory.Name,
                                Brand = await brandRepository.Get(defaultBrand.Id),
                                Description = "Description",
                                Price = 100,
                                Category = categoryRepository.Get(category.Id).Result,
                                Subcategory = await subcategoryRepository.Get(subcategory.Id),
                                IsActive = true
                            };

                            await productRepository.Add(productModel);
                        }
                    }
                }
            }
        }

        public static async Task SeedSpecificationsToBikeCategory(IServiceScope scope)
        {
            var productSpecificationRepository = scope.ServiceProvider.GetService<IProductSpecificationRepository>();

            if (!await productSpecificationRepository.Any())
            {
                var categoryRepository = scope.ServiceProvider.GetService<ICategoryRepository>();
                var productRepository = scope.ServiceProvider.GetService<IProductRepository>();
                var specificationRepository = scope.ServiceProvider.GetService<ISpecificationRepository>();

                var bikeCategory = await categoryRepository.GetCategoryDataByName(BIKE_CATEGORY_NAME);
                var products = await productRepository.GetAllProductsByCategory(bikeCategory.Id);

                foreach (var product in products)
                {
                    var specificationList = await specificationRepository.GetAll();

                    foreach (var spec in specificationList)
                    {
                        var model = new ProductSpecification()
                        {
                            ProductId = product.Id,
                            SpecificationId = spec.Id,
                            Value = "Seed value",
                            IsActive = true
                        };

                        await productSpecificationRepository.Add(model);
                    }
                }
            }

        }
    }
}
