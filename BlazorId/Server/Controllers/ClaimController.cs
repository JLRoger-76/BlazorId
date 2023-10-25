using BlazorId.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorId.Server.Controllers
{
    [ApiController]
    [Route("api/claim")]
    public class ClaimsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ClaimsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUserClaims(string userName)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return NotFound();
                }
                var claims = await _userManager.GetClaimsAsync(user);
                var claimDtos = claims.Select(c => new ClaimDto
                {
                    Type = c.Type,
                    Value = c.Value
                }).ToList();
                return Ok(claimDtos);
            }

        [HttpPost]
        public async Task<IActionResult> AddClaim(string userName,string claimType, string claimValue)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var claim = new Claim(claimType, claimValue);
            var result = await _userManager.AddClaimAsync(user, claim);
            if (result.Succeeded)
            {
                return Ok("Claim added successfully.");
            }
            return BadRequest("Failed to add claim.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClaim(string userName,string claimType,string claimCurrentValue,string claimValue)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var existingClaim = new Claim(claimType, claimCurrentValue);
            var newClaim = new Claim(claimType, claimValue);

            var result = await _userManager.ReplaceClaimAsync(user, existingClaim, newClaim);
            if (result.Succeeded)
            {
                return Ok("Claim updated successfully.");
            }
            return BadRequest("Failed to update claim.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveClaim(string userName,string claimType)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var claim = new Claim(claimType, ""); // Empty value, as it's not used for deletion
            var result = await _userManager.RemoveClaimAsync(user, claim);
            if (result.Succeeded)
            {
                return Ok("Claim removed successfully.");
            }
            return BadRequest("Failed to remove claim.");
        }
    }
}

