using BusinessObject.Models;
using DataAccess.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        [HttpGet("GetAllCategory")]
        public async Task<IEnumerable<Category>> GetCategory() => await _categoryRepository.GetCategories();

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(Category c)
        {
            _categoryRepository.SaveCategory(c);
            return NoContent();
        }

        [HttpGet("Detail/{id}")]
        public async Task<Category?> GetCategoryById(int id) => await _categoryRepository.GetCategoryById(id);

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var c = await _categoryRepository.GetCategoryById(id);
            if (c == null)
            {
                return NotFound("Can not found category to delete");
            }
            await _categoryRepository.DeleteCategory(c);
            return NoContent();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategory(Category c)
        {
            var cTmp = await _categoryRepository.GetCategoryById(c.CategoryId);
            if (cTmp == null)
            {
                return NotFound($"Can not find category have name {c.CategoryName}");
            }
            await _categoryRepository.UpdateCategory(c);
            return NoContent();
        }
    }
}
