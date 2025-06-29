using API.Services;
using BL.Api;
using BL.Models;
using DAL.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
     
        private readonly IUserBl _userService;

        private readonly JwtService _jwtService;


        public AuthController(IUserBl userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            Console.WriteLine($"Trying login: {request?.Name}, {request?.Phone}");

            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Phone))
                return BadRequest("שדות חסרים");

            var user = await _userService.GetUserByNameAndPhone(request.Name, request.Phone);
            if (user == null)
                return Unauthorized("פרטי התחברות שגויים");

            bool isAdmin = user.Name == "admin" && user.Phone == "admin123";

            var token = _jwtService.GenerateToken(user, isAdmin);

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

    }

}
