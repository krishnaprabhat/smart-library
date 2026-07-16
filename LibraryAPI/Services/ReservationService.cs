using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepo;
        private readonly IBookRepository _bookRepo;
        private const int ReservationExpiryHours = 24;

        public ReservationService(IReservationRepository reservationRepo, IBookRepository bookRepo)
        {
            _reservationRepo = reservationRepo;
            _bookRepo = bookRepo;
        }

        public async Task<(bool Success, string Message, ReservationDto? Reservation)> ReserveAsync(int userId, CreateReservationDto dto)
        {
            var book = await _bookRepo.GetByIdAsync(dto.BookId);
            if (book == null) return (false, "Book not found.", null);
            if (book.AvailableCopies > 0) return (false, "Book is available — please borrow directly.", null);

            var reservation = new Reservation
            {
                UserId = userId,
                BookId = dto.BookId,
                ReservedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(ReservationExpiryHours),
                IsFulfilled = false,
                IsCancelled = false
            };

            var created = await _reservationRepo.AddAsync(reservation);
            var full = await _reservationRepo.GetByIdAsync(created.Id);
            return (true, "Reservation created successfully.", MapToDto(full!));
        }

        public async Task<(bool Success, string Message)> CancelAsync(int reservationId, int userId)
        {
            var reservation = await _reservationRepo.GetByIdAsync(reservationId);
            if (reservation == null) return (false, "Reservation not found.");
            if (reservation.UserId != userId) return (false, "Unauthorized.");
            if (reservation.IsCancelled) return (false, "Already cancelled.");

            reservation.IsCancelled = true;
            await _reservationRepo.SaveChangesAsync();
            return (true, "Reservation cancelled.");
        }

        public async Task<List<ReservationDto>> GetMyReservationsAsync(int userId)
        {
            var records = await _reservationRepo.GetByUserIdAsync(userId);
            return records.Select(MapToDto).ToList();
        }

        public async Task<List<ReservationDto>> GetAllActiveAsync()
        {
            var records = await _reservationRepo.GetAllActiveAsync();
            return records.Select(MapToDto).ToList();
        }

        private static ReservationDto MapToDto(Reservation r) => new()
        {
            Id = r.Id,
            BookId = r.BookId,
            BookTitle = r.Book?.Title ?? "",
            BookAuthor = r.Book?.Author ?? "",
            StudentName = r.User?.Name ?? "",
            StudentId = r.User?.StudentId ?? "",
            ReservedAt = r.ReservedAt,
            ExpiresAt = r.ExpiresAt,
            IsFulfilled = r.IsFulfilled,
            IsCancelled = r.IsCancelled
        };
    }
}
