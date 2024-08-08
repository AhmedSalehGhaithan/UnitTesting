using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserInterface _userInterface) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Create(User user)
        {
            var result = await _userInterface.CreateAsync(user);
            if (result)
                return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
            else
                return BadRequest();
        }

        [HttpGet("Get")]

        public async Task<IActionResult> GetUsers()
        {
            var users = await _userInterface.GetAllAsync();
            if (!users.Any()) 
                return NotFound();
            else
                return Ok(users);
        }

        [HttpGet("Get-single")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userInterface.GetById(id);
            if(user is null)
                return NotFound();
            else
                return Ok(user);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var result = await _userInterface.UpdateAsync(user);
            if(result)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userInterface.DeleteAsync(id);
            if (result)
                return NoContent();
            else
                return NotFound();
        }

    }
}
