using LibraryAPI.DTOs;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/borrow")]
    [Authorize]
    public class BorrowController : ControllerBase
    {
        private readonly BorrowService _borrowService;

        public BorrowController(BorrowService borrowService) => _borrowService = borrowService;

        private int GetUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpPost]
        public async Task<IActionResult> Borrow([FromBody] BorrowRequestDto dto)
        {
            var (success, message, record) = await _borrowService.BorrowAsync(GetUserId(), dto);
            if (!success) return BadRequest(new { message });
            return Ok(record);
        }

        [HttpPost("return/{id}")]
        public async Task<IActionResult> Return(int id)
        {
            // Admin/Librarian can return on behalf of any user
            var isAdminOrLibrarian = User.IsInRole("Admin") || User.IsInRole("Librarian");
            var userId = isAdminOrLibrarian ? 0 : GetUserId();
            var (success, message, record) = await _borrowService.ReturnAsync(id, userId, isAdminOrLibrarian);
            if (!success) return BadRequest(new { message });
            return Ok(record);
        }

        [HttpGet("my")]
        public async Task<IActionResult> MyBorrows()
        {
            var borrows = await _borrowService.GetMyBorrowsAsync(GetUserId());
            return Ok(borrows);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> AllBorrows()
        {
            var borrows = await _borrowService.GetAllActiveBorrowsAsync();
            return Ok(borrows);
        }
    }
}
