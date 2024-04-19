using Data.Interface.Models;
using Data.Interface.Models.enums;
using Data.Interface.Repositories;
using Data.Services.Interfaces.UsersService;

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

        private static List<string> _categories = new List<string> { BIKE_CATEGORY_NAME, ACCESSORY_CATEGORY_NAME, SPARE_CATEGORY_NAME, TOOL_CATEGORY_NAME, EQUIPMENT_CATEGORY_NAME, SCOOTER_CATEGORY_NAME };
        private static List<string> _bikeSubcategories = new List<string> { "Горные велосипеды", "Женские велосипеды", "Детские велосипеды", "Подростковые велосипеды", "Городские велосипеды", "Шоссейные велосипеды", "Складные велосипеды", "Велосипеды BMX",};
        private static List<string> _accessorySubcategories = new List<string> { "Велозамки", "Освещение", "Насосы", "Щитки", "Багажники", "Велокомпьютеры", "Велосумки", "Грипсы", "Подножки" };
        private static List<string> _spareSubcategories = new List<string> { "Велокамеры", "Велопокрышки", "Переключатели", "Педали", "Вилки", "Каретки, системы и шатуны", "Колеса и части", "Манетки и шифтеры", "Рулевое управление", "Подшипники", "Седла и части", "Тормозная система", "Цепи" };
        private static List<string> _toolSubcategories = new List<string> { "Многофункциональные инструменты", "Уход за велосипедом", "Велоаптечки"};
        private static List<string> _equiomentSubcategories = new List<string> { "Шлемы", "Перчатки"};
        private static List<string> _scooterSubcategories = new List<string> { "Взрослые самокаты", "Детские самокаты", "Трюковые самокаты", "Запчасти для самоката"};

        private static List<string> _bikeSpecifications = new List<string>{ "Диаметр колес", "Материал рамы", "Размер рамы", "Количество скоростей", "Манетки", "Вилка", "Руль", "Седло" };

        public static void Seed(WebApplication webApplication)
        {
            using(var scope = webApplication.Services.CreateScope())
            {
                SeedCategory(scope);
                SeedSubcategoriesForCategories(scope);
                SeedSpecifications(scope);
                SeedUsers(scope);
            }
        }


        public static void SeedCategory(IServiceScope scope)
        {
            var categoryRepository = scope.ServiceProvider.GetService<ICategoryRepository>();

            if (!categoryRepository.Any())
            {
                foreach(var item in _categories)
                {
                    var model = new Category()
                    {
                        Name = item,
                        IsActive = true
                    };

                    categoryRepository.Add(model);
                }
            }
        }

        public static void SeedSubcategoriesForCategories(IServiceScope scope)
        {
            var categoryRepository = scope.ServiceProvider.GetService<ICategoryRepository>();
            var subcategoryRepository = scope.ServiceProvider.GetService<ISubcategoryRepository>();

            if (!subcategoryRepository.Any())
            {
                var bikeCategory = categoryRepository.GetByName(BIKE_CATEGORY_NAME);
                foreach (var item in _bikeSubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = bikeCategory,
                        Name = item,
                        IsActive = true
                    };

                    subcategoryRepository.Add(model);
                }

                var accessoryCategory = categoryRepository.GetByName(ACCESSORY_CATEGORY_NAME);
                foreach (var item in _accessorySubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = accessoryCategory,
                        Name = item,
                        IsActive = true
                    };

                    subcategoryRepository.Add(model);
                }

                var spareCategory = categoryRepository.GetByName(SPARE_CATEGORY_NAME);
                foreach (var item in _spareSubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = spareCategory,
                        Name = item,
                        IsActive = true
                    };

                    subcategoryRepository.Add(model);
                }

                var toolCategory = categoryRepository.GetByName(TOOL_CATEGORY_NAME);
                foreach (var item in _toolSubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = toolCategory,
                        Name = item,
                        IsActive = true
                    };

                    subcategoryRepository.Add(model);
                }

                var equipmentCategory = categoryRepository.GetByName(EQUIPMENT_CATEGORY_NAME);
                foreach (var item in _equiomentSubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = equipmentCategory,
                        Name = item,
                        IsActive = true
                    };

                    subcategoryRepository.Add(model);
                }

                var scooterCategory = categoryRepository.GetByName(SCOOTER_CATEGORY_NAME);
                foreach (var item in _scooterSubcategories)
                {
                    var model = new Subcategory()
                    {
                        Category = scooterCategory,
                        Name = item,
                        IsActive = true
                    };

                    subcategoryRepository.Add(model);
                }
            }
        }

        public static void SeedSpecifications(IServiceScope scope)
        {
            var categoryRepository = scope.ServiceProvider.GetService<ICategoryRepository>();
            var specificationRepository = scope.ServiceProvider.GetService<ISpecificationRepository>();

            if (!specificationRepository.Any())
            {
                var bikeCategory = categoryRepository.GetByName(BIKE_CATEGORY_NAME);

                foreach (var item in _bikeSpecifications)
                {
                    var model = new Specification()
                    {
                        Name = item,
                        Category = bikeCategory,
                        IsActive = true
                    };

                    specificationRepository.Add(model);
                }
            }
        }

        public static void SeedUsers(IServiceScope scope)
        {
            var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
            var userService = scope.ServiceProvider.GetService<IUserService>();

            if (!userRepository.IsUsernameExist(ADMIN_NAME_ROLE))
            {
                userService.Register(ADMIN_NAME_ROLE, ADMIN_EMAIL, ADMIN_PASSWORD, SiteRole.Admin );
            }
        }
    }
}
