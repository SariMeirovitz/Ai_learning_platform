using BL.Api;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userBl.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(_userBl.GetAll());
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] BLUser user)
        {
            var created = _userBl.Create(user);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] LoginRequest request)
        {
            var user = _userBl.GetAll().FirstOrDefault(u => u.Phone == request.Phone);
            if (user == null)
                return Unauthorized("User not found");
            return Ok(user);
        }

        [HttpPut("{id}")]
        public ActionResult<User> Update(int id, [FromBody] BLUser user)
        {
            if (id != user.Id)
                return BadRequest();
            var updated = _userBl.Update(user);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userBl.Delete(id);
            return NoContent();
        }
    }
}
