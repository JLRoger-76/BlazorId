using BlazorId.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BlazorId.Server.Controllers
{
    [Route("api/userroles")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserRoleController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            return Ok(userRoles);
        }

        [HttpPost]
        public async Task<ActionResult> AddUserRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }



            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok("Role added successfully");
            }

            return BadRequest("Failed to add role to user");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserRole(string userId, string oldRoleName, string newRoleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var oldRoleExists = await _userManager.IsInRoleAsync(user, oldRoleName);
            if (!oldRoleExists)
            {
                return BadRequest("User does not have the specified role");
            }



            var resultRemove = await _userManager.RemoveFromRoleAsync(user, oldRoleName);
            var resultAdd = await _userManager.AddToRoleAsync(user, newRoleName);

            if (resultRemove.Succeeded && resultAdd.Succeeded)
            {
                return Ok("Role updated successfully");
            }

            return BadRequest("Failed to update user role");
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveUserRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }



            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok("Role removed successfully");
            }

            return BadRequest("Failed to remove role from user");
        }
    }
}