using CategoriesApi.Models.Categories;
using CategoriesApi.Models.Filters;
using CategoriesApi.Services.Categories.ICategories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CategoriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public ActionResult Get([FromQuery] CategoryFilter filter)
        {
            IList<Category> categories = _categoryService.Get(filter);

            return categories.Any() ? Ok(categories) : NotFound("Categories not found.");
        }

        [HttpGet("Detail")]
        public ActionResult Detail([FromQuery] CategoryFilter filter)
        {
            CategoryDetail category = _categoryService.Detail(filter);

            return category is not null ? Ok(category) : NotFound("Category not found.");
        }

        [HttpGet("CategoryChildren/{id}")]
        public ActionResult CategoryChildren([FromRoute] string id)
        {
            IList<Category> categories = _categoryService.CategoryChildren(id);

            return categories.Any() ? Ok(categories) : NotFound("Category children not found.");
        }

        [HttpPost]
        public ActionResult Create([FromBody] Category category)
        {
            return _categoryService.Create(category) ? Ok("Category registered successfully") : Problem("Problem creating category.");
        }

        [HttpPatch("UpdateVisibility/{id}")]
        public ActionResult Visible([FromRoute] string id, bool visible)
        {
            return _categoryService.Visible(id, visible) ? Ok("Status updated successfully") : NotFound("Could not update status");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (_categoryService.Delete(id))
            {
                return Ok("Category deleted successfully.");
            }

            return NotFound("Problem deleting category.");
        }
    }
}
