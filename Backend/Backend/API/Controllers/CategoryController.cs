using BL.Api;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBl _categoryBl;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IBl bl, ILogger<CategoryController> logger)
        {
            _categoryBl = bl.Category;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                var category = _categoryBl.GetById(id);
                if (category == null)
                {
                    _logger.LogWarning("Category with Id {CategoryId} not found.", id);
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching category with Id {CategoryId}.", id);
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            try
            {
                var categories = _categoryBl.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all categories.");
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpPost]
        public ActionResult<Category> Create([FromBody] BLCategory category)
        {
            try
            {
                if (category == null)
                {
                    _logger.LogWarning("Attempted to create a category with null data.");
                    return BadRequest("נתונים לא תקינים");
                }

                var created = _categoryBl.Create(category);
                _logger.LogInformation("Category with Id {CategoryId} created successfully.", created.Id);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating category.");
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Category> Update(int id, [FromBody] BLCategory category)
        {
            try
            {
                if (id != category.Id)
                {
                    _logger.LogWarning("Mismatched Id: requested Id {RequestedId} does not match category Id {CategoryId}.", id, category.Id);
                    return BadRequest();
                }

                var updated = _categoryBl.Update(category);
                if (updated == null)
                {
                    _logger.LogWarning("Category with Id {CategoryId} not found for update.", id);
                    return NotFound();
                }

                _logger.LogInformation("Category with Id {CategoryId} updated successfully.", updated.Id);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating category with Id {CategoryId}.", id);
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var category = _categoryBl.GetById(id);
                if (category == null)
                {
                    _logger.LogWarning("Category with Id {CategoryId} not found for deletion.", id);
                    return NotFound();
                }

                _categoryBl.Delete(id);
                _logger.LogInformation("Category with Id {CategoryId} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting category with Id {CategoryId}.", id);
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }
    }
}
