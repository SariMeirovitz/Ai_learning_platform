using BL.Api;
using BL.Models;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IUserBl _userBl;
        private readonly ILogger<AdminController> _logger;

        // קונסטרוקטור המכניס את הבל וה-ILogger
        public AdminController(IUserBl userBl, ILogger<AdminController> logger)
        {
            _userBl = userBl;
            _logger = logger;
        }

        [HttpGet("all-users-with-prompts")]
        public async Task<IActionResult> GetAllUsersWithPrompts()
        {
            try
            {
                // בדיקת אם המשתמש הוא מנהל
                var isAdmin = User.FindFirst("isAdmin")?.Value == "True";
                if (!isAdmin)
                {
                    _logger.LogWarning("Unauthorized access attempt by user: {user}", User.Identity.Name);
                    return Forbid();  // החזרת שגיאה אם המשתמש לא מנהל
                }

                // שליפת כל המשתמשים עם הפקודות שלהם
                var users = await _userBl.GetAllUsersWithPrompts();
                if (users == null || !users.Any())
                {
                    _logger.LogInformation("No users found.");
                    return NotFound("No users found.");
                }

                // החזרת המשתמשים
                return Ok(users);
            }
            catch (Exception ex)
            {
                // תיעוד השגיאה בלוג
                _logger.LogError(ex, "An error occurred while retrieving users with prompts.");
                return StatusCode(500, "Internal server error");  // החזרת שגיאה כללית
            }
        }
    }
}
