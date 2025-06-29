using BL.Api;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PromptController : ControllerBase
    {
        private readonly IPromptBl _promptBl;

        public PromptController(IBl bl)
        {
            _promptBl = bl.Prompt;
        }

        [HttpGet("my-history")]
        public ActionResult<IEnumerable<Prompt>> GetMyHistory()
        {
            // שולפים את ה־userId מהטוקן
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            // משתמשים ב־BL כדי לקבל את הפרומפטים לפי userId
            var prompts = _promptBl.GetByUserId(userId);

            return Ok(prompts);
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
