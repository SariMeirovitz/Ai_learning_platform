using BL.Api;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryBl _subCategoryBl;

        public SubCategoryController(IBl bl)
        {
            _subCategoryBl = bl.SubCategory;
        }

        [HttpGet("{id}")]
        public ActionResult<SubCategory> Get(int id)
        {
            var subCategory = _subCategoryBl.GetById(id);
            if (subCategory == null)
                return NotFound();
            return Ok(subCategory);
        }

        [HttpGet]
        public ActionResult<IEnumerable<SubCategory>> GetAll()
        {
            return Ok(_subCategoryBl.GetAll());
        }

        [HttpPost]
        public ActionResult<SubCategory> Create([FromBody] BLSubCategory subCategory)
        {
            var created = _subCategoryBl.Create(subCategory);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<SubCategory> Update(int id, [FromBody] BLSubCategory subCategory)
        {
            if (id != subCategory.Id)
                return BadRequest();
            var updated = _subCategoryBl.Update(subCategory);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _subCategoryBl.Delete(id);
            return NoContent();
        }
    }
}
