using Microsoft.AspNetCore.Mvc;
using Gutenburg_Server.Models;
using Gutenburg_Server.DTOs;
using Gutenburg_Server.Services;

namespace Gutenburg_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var dtos = users.Select(user => new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var dto = new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserDTO dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = "123456", // „·«ÕŸ…: ÌÃ» ·«Õﬁ« «” Œœ«„ hashing
                Role = Enum.Parse<Role>(dto.Role)
            };

            var created = await _userService.CreateAsync(user);

            dto.UserId = created.UserId;
            return CreatedAtAction(nameof(GetById), new { id = dto.UserId }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Update(int id, [FromBody] UserDTO dto)
        {
            var existing = await _userService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.Role = Enum.Parse<Role>(dto.Role);

            var updated = await _userService.UpdateAsync(existing);

            return Ok(new UserDTO
            {
                UserId = updated.UserId,
                Name = updated.Name,
                Email = updated.Email,
                Role = updated.Role.ToString()
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
