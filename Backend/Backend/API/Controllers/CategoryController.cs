using BL.Api;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBl _categoryBl;

        public CategoryController(IBl bl)
        {
            _categoryBl = bl.Category;
        }

        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var category = _categoryBl.GetById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return Ok(_categoryBl.GetAll());
        }

        [HttpPost]
        public ActionResult<Category> Create([FromBody] BLCategory category)
        {
            var created = _categoryBl.Create(category);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<Category> Update(int id, [FromBody] BLCategory category)
        {
            if (id != category.Id)
                return BadRequest();
            var updated = _categoryBl.Update(category);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryBl.Delete(id);
            return NoContent();
        }
    }
}
