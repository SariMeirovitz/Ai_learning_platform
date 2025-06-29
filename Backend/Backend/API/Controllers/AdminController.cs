using BL.Api;
using BL.Models;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IUserBl _userBl;

        public AdminController(IUserBl userBl)
        {
            _userBl = userBl;
        }

        [HttpGet("all-users-with-prompts")]
        public async Task<IActionResult> GetAllUsersWithPrompts()
        {
            var isAdmin = User.FindFirst("isAdmin")?.Value == "True";
            if (!isAdmin)
                return Forbid();

            var users = await _userBl.GetAllUsersWithPrompts();
            return Ok(users);
        }
    }
}
