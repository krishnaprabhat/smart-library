using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;
        private readonly BorrowService _borrowService;

        public DashboardController(DashboardService dashboardService, BorrowService borrowService)
        {
            _dashboardService = dashboardService;
            _borrowService = borrowService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _dashboardService.GetStatsAsync();
            return Ok(stats);
        }

        [HttpGet("active-borrows")]
        public async Task<IActionResult> GetActiveBorrows()
        {
            // Reuse BorrowRepository through a workaround — inject IBorrowRepository via BorrowService
            // We'll query directly through context here for simplicity
            var borrows = await _borrowService.GetAllActiveBorrowsAsync();
            return Ok(borrows);
        }
    }
}
