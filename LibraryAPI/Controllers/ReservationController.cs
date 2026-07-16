using LibraryAPI.DTOs;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService) =>
            _reservationService = reservationService;

        private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpPost]
        public async Task<IActionResult> Reserve([FromBody] CreateReservationDto dto)
        {
            var (success, message, reservation) = await _reservationService.ReserveAsync(GetUserId(), dto);
            if (!success) return BadRequest(new { message });
            return Ok(reservation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var (success, message) = await _reservationService.CancelAsync(id, GetUserId());
            if (!success) return BadRequest(new { message });
            return Ok(new { message });
        }

        [HttpGet("my")]
        public async Task<IActionResult> MyReservations()
        {
            var items = await _reservationService.GetMyReservationsAsync(GetUserId());
            return Ok(items);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> GetAllActive()
        {
            var items = await _reservationService.GetAllActiveAsync();
            return Ok(items);
        }
    }
}
