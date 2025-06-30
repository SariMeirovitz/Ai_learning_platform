using BL.Api;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBl _userBl;
        private readonly ILogger<UserController> _logger;

        public UserController(IBl bl, ILogger<UserController> logger)
        {
            _userBl = bl.User;
            _logger = logger;
        }

        // יצירת משתמש חדש - פתוח ללא צורך באישור
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<User> Create([FromBody] BLUser user)
        {
            try
            {
                var created = _userBl.Create(user);
                _logger.LogInformation("User with Id {UserId} created successfully.", created.Id);
                return CreatedAtAction(nameof(GetMyDetails), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new user.");
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        // קבלת פרטי המשתמש המחובר (JWT)
        [HttpGet("me")]
        [Authorize]
        public ActionResult<User> GetMyDetails()
        {
            try
            {
                var userId = GetUserIdFromToken();
                var user = _userBl.GetById(userId);
                if (user == null)
                {
                    _logger.LogWarning("User with Id {UserId} not found.", userId);
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user details for user Id {UserId}.", GetUserIdFromToken());
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        // קבלת כל המשתמשים - רק למנהלים
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            try
            {
                if (!IsAdmin())
                {
                    _logger.LogWarning("Unauthorized access attempt to view all users.");
                    return Forbid("רק מנהלים מורשים לצפות בכל המשתמשים");
                }

                var users = _userBl.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all users.");
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        // עדכון פרטי המשתמש עצמו בלבד
        [HttpPut]
        [Authorize]
        public ActionResult<User> Update([FromBody] BLUser user)
        {
            try
            {
                var userId = GetUserIdFromToken();

                if (user.Id != userId)
                {
                    _logger.LogWarning("User with Id {UserId} attempted to update another user's details.", userId);
                    return Forbid("אסור לעדכן משתמש אחר");
                }

                var updated = _userBl.Update(user);
                if (updated == null)
                {
                    _logger.LogWarning("User with Id {UserId} not found for update.", userId);
                    return NotFound();
                }

                _logger.LogInformation("User with Id {UserId} updated successfully.", userId);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user with Id {UserId}.", GetUserIdFromToken());
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        // מחיקת משתמש - רק המשתמש עצמו או מנהל
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = GetUserIdFromToken();

                if (id != userId && !IsAdmin())
                {
                    _logger.LogWarning("Unauthorized deletion attempt by user with Id {UserId}.", userId);
                    return Forbid("אין לך הרשאה למחוק משתמש זה");
                }

                _userBl.Delete(id);
                _logger.LogInformation("User with Id {UserId} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user with Id {UserId}.", id);
                return StatusCode(500, "שגיאה פנימית בשרת");
            }
        }

        // --- פונקציות עזר ---

        private int GetUserIdFromToken()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        private bool IsAdmin()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            return role == "admin";
        }
    }
}
