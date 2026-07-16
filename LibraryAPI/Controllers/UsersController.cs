using LibraryAPI.DTOs;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserAdminService _userAdminService;

        public UsersController(UserAdminService userAdminService) => _userAdminService = userAdminService;

        private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpGet]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userAdminService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("{id}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateUserRoleDto dto)
        {
            var success = await _userAdminService.UpdateRoleAsync(id, dto.Role);
            return success ? NoContent() : BadRequest(new { message = "Invalid user or role." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userAdminService.DeleteUserAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            var success = await _userAdminService.UpdateProfileAsync(GetUserId(), dto);
            return success ? NoContent() : NotFound();
        }
    }
}
