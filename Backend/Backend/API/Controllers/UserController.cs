using BL.Api;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBl _userBl;

        public UserController(IBl bl)
        {
            _userBl = bl.User;
        }

        // יצירת משתמש חדש - פתוח ללא צורך באישור
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<User> Create([FromBody] BLUser user)
        {
            var created = _userBl.Create(user);
            return CreatedAtAction(nameof(GetMyDetails), new { id = created.Id }, created);
        }

        // קבלת פרטי המשתמש המחובר (JWT)
        [HttpGet("me")]
        [Authorize]
        public ActionResult<User> GetMyDetails()
        {
            var userId = GetUserIdFromToken();
            var user = _userBl.GetById(userId);
            return user != null ? Ok(user) : NotFound();
        }

        // קבלת כל המשתמשים - רק למנהלים
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            if (!IsAdmin())
                return Forbid("רק מנהלים מורשים לצפות בכל המשתמשים");

            return Ok(_userBl.GetAll());
        }

        // עדכון פרטי המשתמש עצמו בלבד
        [HttpPut]
        [Authorize]
        public ActionResult<User> Update([FromBody] BLUser user)
        {
            var userId = GetUserIdFromToken();

            if (user.Id != userId)
                return Forbid("אסור לעדכן משתמש אחר");

            var updated = _userBl.Update(user);
            return Ok(updated);
        }

        // מחיקת משתמש - רק המשתמש עצמו או מנהל
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = GetUserIdFromToken();
            if (id != userId && !IsAdmin())
                return Forbid("אין לך הרשאה למחוק משתמש זה");

            _userBl.Delete(id);
            return NoContent();
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
