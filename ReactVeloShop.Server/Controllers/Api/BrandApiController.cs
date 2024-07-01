using Data.Interface.DataModels.Brands;
using Data.Services.Interfaces.BrandsService;
using Data.Services.Interfaces.CategoriesService;
using Microsoft.AspNetCore.Mvc;

namespace ReactVeloShop.Server.Controllers.Api
{

    [ApiController]
    [Route("/api/brands")]
    public class BrandApiController : ControllerBase
    {
        private IBrandService _brandService;
        private ICategoryService _categoryService;
        public BrandApiController(IBrandService brandService, ICategoryService categoryService)
        {
            _brandService = brandService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brandsData = await _brandService.GetAllBrandData();
            return Ok(brandsData);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetBrandyById(string name)
        {
            var brandData = await _brandService.GetBrandByName(name);
            return Ok(brandData);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetBrandyByCategoryId(int id)
        {
            var brandData = await _brandService.GetBrandsByCategoryId(id);
            return Ok(brandData);
        }

        [Route("bikeBrands")]
        [HttpGet]
        public async Task<IActionResult> GetBrandyByBikeCategory()
        {
            var bikeCategory = await _categoryService.GetCategoryByName("Велосипеды");
            var brandData = await _brandService.GetBrandsByCategoryId(bikeCategory.Id);
            return Ok(brandData);
        }

        [HttpPost]
        public async Task<IActionResult> CrateBrand(BrandData data)
        {
            var brandData = await _brandService.CreateBrand(data);
            return Ok(brandData);
        }
    }
}
