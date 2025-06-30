using API.Services;
using BL.Api;
using BL.Models;
using DAL.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserBl _userService;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserBl userService, JwtService jwtService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // לוג ניסיון כניסה
            _logger.LogInformation("Trying to log in with Name: {Name}, Phone: {Phone}", request?.Name, request?.Phone);

            // בדיקת קלט
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Phone))
            {
                _logger.LogWarning("Missing fields in login attempt: Name or Phone is null or empty.");
                return BadRequest("שדות חסרים");
            }

            try
            {
                // חיפוש המשתמש
                var user = await _userService.GetUserByNameAndPhone(request.Name, request.Phone);

                if (user == null)
                {
                    _logger.LogWarning("Invalid login attempt: user not found with provided Name: {Name} and Phone: {Phone}.", request.Name, request.Phone);
                    return Unauthorized("פרטי התחברות שגויים");
                }

                bool isAdmin = user.Name == "admin" && user.Phone == "admin123";
                var token = _jwtService.GenerateToken(user, isAdmin);

                // לוג הצלחה
                _logger.LogInformation("User {UserId} logged in successfully.", user.Id);

                return Ok(new
                {
                    token,
                    user = new
                    {
                        user.Id,
                        user.Name,
                        user.Phone,
                        isAdmin
                    }
                });
            }
            catch (Exception ex)
            {
                // לוג שגיאה אם קרתה
                _logger.LogError(ex, "An error occurred during the login attempt for Name: {Name}, Phone: {Phone}.", request.Name, request.Phone);
                return StatusCode(StatusCodes.Status500InternalServerError, "שגיאה פנימית בשרת");
            }
        }
    }
}
