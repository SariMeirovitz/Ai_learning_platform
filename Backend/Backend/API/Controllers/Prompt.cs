using BL.Api;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromptController : ControllerBase
    {
        private readonly IPromptBl _promptBl;

        public PromptController(IBl bl)
        {
            _promptBl = bl.Prompt;
        }

        [HttpGet("{id}")]
        public ActionResult<Prompt> Get(int id)
        {
            var prompt = _promptBl.GetById(id);
            if (prompt == null)
                return NotFound();
            return Ok(prompt);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Prompt>> GetAll()
        {
            return Ok(_promptBl.GetAll());
        }

        [HttpPost]
        public ActionResult<Prompt> Create([FromBody] BLPrompt prompt)
        {
            var created = _promptBl.Create(prompt);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public ActionResult<Prompt> Update(int id, [FromBody] BLPrompt prompt)
        {
            if (id != prompt.Id)
                return BadRequest();
            var updated = _promptBl.Update(prompt);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _promptBl.Delete(id);
            return NoContent();
        }
    }
}
