using BlazorId.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorId.Server.Controllers
{ 
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationUser>> GetUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationUser>> CreateUser(ApplicationUser user)
    {
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        return BadRequest(result.Errors);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, ApplicationUser user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        var existingUser = await _userManager.FindByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.UserName = user.UserName;
        // Update other user properties as needed

        var result = await _userManager.UpdateAsync(existingUser);
        if (result.Succeeded)
        {
            return NoContent();
        }
        return BadRequest(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return NoContent();
        }
        return BadRequest(result.Errors);
    }
}

}