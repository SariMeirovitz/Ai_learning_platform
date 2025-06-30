using BL.Api;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryBl _subCategoryBl;
        private readonly ILogger<SubCategoryController> _logger;

        public SubCategoryController(IBl bl, ILogger<SubCategoryController> logger)
        {
            _subCategoryBl = bl.SubCategory;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<SubCategory> Get(int id)
        {
            try
            {
                var subCategory = _subCategoryBl.GetById(id);
                if (subCategory == null)
                {
                    _logger.LogWarning("SubCategory with Id {SubCategoryId} not found.", id);
                    return NotFound();
                }
                return Ok(subCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching subcategory with Id {SubCategoryId}.", id);
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<SubCategory>> GetAll()
        {
            try
            {
                var subCategories = _subCategoryBl.GetAll();
                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all subcategories.");
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpPost]
        public ActionResult<SubCategory> Create([FromBody] BLSubCategory subCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state while creating subcategory.");
                    return BadRequest(ModelState);
                }

                var created = _subCategoryBl.Create(subCategory);
                _logger.LogInformation("SubCategory with Id {SubCategoryId} created successfully.", created.Id);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating subcategory.");
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<SubCategory> Update(int id, [FromBody] BLSubCategory subCategory)
        {
            try
            {
                if (id != subCategory.Id)
                {
                    _logger.LogWarning("Mismatched Id: requested Id {RequestedId} does not match subcategory Id {SubCategoryId}.", id, subCategory.Id);
                    return BadRequest();
                }

                var updated = _subCategoryBl.Update(subCategory);
                if (updated == null)
                {
                    _logger.LogWarning("SubCategory with Id {SubCategoryId} not found for update.", id);
                    return NotFound();
                }

                _logger.LogInformation("SubCategory with Id {SubCategoryId} updated successfully.", updated.Id);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating subcategory with Id {SubCategoryId}.", id);
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var subCategory = _subCategoryBl.GetById(id);
                if (subCategory == null)
                {
                    _logger.LogWarning("SubCategory with Id {SubCategoryId} not found for deletion.", id);
                    return NotFound();
                }

                _subCategoryBl.Delete(id);
                _logger.LogInformation("SubCategory with Id {SubCategoryId} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting subcategory with Id {SubCategoryId}.", id);
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }
    }
}
