using BL.Api;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PromptController : ControllerBase
    {
        private readonly IPromptBl _promptBl;
        private readonly ILogger<PromptController> _logger;

        public PromptController(IBl bl, ILogger<PromptController> logger)
        {
            _promptBl = bl.Prompt;
            _logger = logger;
        }

        [HttpGet("my-history")]
        public ActionResult<IEnumerable<Prompt>> GetMyHistory()
        {
            try
            {
                // שולפים את ה־userId מהטוקן
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    _logger.LogWarning("UserId claim not found in token.");
                    return Unauthorized();
                }

                if (!int.TryParse(userIdClaim.Value, out int userId))
                {
                    _logger.LogWarning("Invalid userId claim value: {UserId}", userIdClaim.Value);
                    return Unauthorized();
                }

                // משתמשים ב־BL כדי לקבל את הפרומפטים לפי userId
                var prompts = _promptBl.GetByUserId(userId);
                if (prompts == null || !prompts.Any())
                {
                    _logger.LogInformation("No prompts found for UserId {UserId}.", userId);
                }

                return Ok(prompts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching prompts for user.");
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Prompt> Get(int id)
        {
            try
            {
                var prompt = _promptBl.GetById(id);
                if (prompt == null)
                {
                    _logger.LogWarning("Prompt with Id {PromptId} not found.", id);
                    return NotFound();
                }
                return Ok(prompt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching prompt with Id {PromptId}.", id);
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Prompt>> GetAll()
        {
            try
            {
                var prompts = _promptBl.GetAll();
                return Ok(prompts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all prompts.");
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpPost]
        public ActionResult<Prompt> Create([FromBody] BLPrompt prompt)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state while creating prompt.");
                    return BadRequest(ModelState);
                }

                var created = _promptBl.Create(prompt);
                _logger.LogInformation("Prompt with Id {PromptId} created successfully.", created.Id);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating prompt.");
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Prompt> Update(int id, [FromBody] BLPrompt prompt)
        {
            try
            {
                if (id != prompt.Id)
                {
                    _logger.LogWarning("Mismatched Id: requested Id {RequestedId} does not match prompt Id {PromptId}.", id, prompt.Id);
                    return BadRequest();
                }

                var updated = _promptBl.Update(prompt);
                if (updated == null)
                {
                    _logger.LogWarning("Prompt with Id {PromptId} not found for update.", id);
                    return NotFound();
                }

                _logger.LogInformation("Prompt with Id {PromptId} updated successfully.", updated.Id);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating prompt with Id {PromptId}.", id);
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var prompt = _promptBl.GetById(id);
                if (prompt == null)
                {
                    _logger.LogWarning("Prompt with Id {PromptId} not found for deletion.", id);
                    return NotFound();
                }

                _promptBl.Delete(id);
                _logger.LogInformation("Prompt with Id {PromptId} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting prompt with Id {PromptId}.", id);
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        [HttpPost("submit")]
        public async Task<ActionResult<Prompt>> SubmitAsync([FromBody] BLPrompt prompt)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state while submitting prompt.");
                    return BadRequest(ModelState);
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    _logger.LogWarning("UserId claim not found or invalid in token during prompt submission.");
                    return Unauthorized();
                }

                prompt.UserId = userId;

                var created = await _promptBl.SubmitPromptAsync(prompt);
                _logger.LogInformation("Prompt with Id {PromptId} submitted successfully.", created.Id);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while submitting prompt.");
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }
    }
}
