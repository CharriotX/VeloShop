using Data.Interface.DataModels;
using Data.Services.Interfaces.CategoriesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReactVeloShop.Server.Controllers.Api
{

    [ApiController]
    [Route("/api/category")]
    public class CategoryApiController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesWithAllSubcategories()
        {
            var models = await _categoryService.GetAllCategoriesWithSubcategories();
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var model = await _categoryService.GetCategoryById(id);
            return Ok(model);
        }
    }
}
