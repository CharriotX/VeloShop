using Data.Interface.DataModels.Subcategories;
using Data.Interface.Repositories;
using Data.Services.Interfaces.CategoriesService;
using Data.Services.Interfaces.SubcategoriesService;
using Data.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace ReactVeloShop.Server.Controllers.Api
{
    [Route("api/subcategory")]
    [ApiController]
    public class SubcategoryApiController : ControllerBase
    {
        private ICategoryService _categoryService;
        private ISubcategoryService _subcategoryService;
        public SubcategoryApiController(ICategoryService categoryService, ISubcategoryService subcategoryService)
        {
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
        }

        [HttpGet("getSubcategories/{categoryId}")]
        public async Task<IActionResult> GetSubcategoriesByCategoryId(int categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);

            var subcategories = category.Subcategories;
            
            return Ok(subcategories);
        }

        [HttpGet("{subcategoryId}")]
        public async Task<IActionResult> GetSubcategoriesById(int subcategoryId)
        {
            var subcategory = await _subcategoryService.GetSubcategory(subcategoryId);

            return Ok(subcategory);
        }

        [HttpPost]
        public IActionResult AddNewSubсategory(NewSubcategoryData data)
        {
            _subcategoryService.AddNewSubcategory(data);

            return Ok();
        }

        [HttpDelete]
        public IActionResult RemoveSubсategory([FromBody] int id)
        {
            if (_subcategoryService.GetSubcategory(id) == null)
            {
                return NotFound();
            }

            _subcategoryService.RemoveSubcategory(id);

            return Ok();
        }
    }
}
