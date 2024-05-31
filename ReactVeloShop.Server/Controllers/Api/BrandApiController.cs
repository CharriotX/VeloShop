using Data.Interface.DataModels.Brands;
using Data.Services.Interfaces.BrandsService;
using Microsoft.AspNetCore.Mvc;

namespace ReactVeloShop.Server.Controllers.Api
{

    [ApiController]
    [Route("/api/brand")]
    public class BrandApiController : ControllerBase
    { 
        private IBrandService _brandService;
        public BrandApiController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brandsData = await _brandService.GetAllBrandData();
            return Ok(brandsData);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandyById(string name)
        {
            var brandData = await _brandService.GetBrandByName(name);
            return Ok(brandData);
        }

        [HttpGet("/category/{id}")]
        public async Task<IActionResult> GetBrandyByCategoryId(int id)
        {
            var brandData = await _brandService.GetBrandsByCategoryId(id);
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
