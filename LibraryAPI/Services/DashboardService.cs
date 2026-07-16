using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class DashboardService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IUserRepository _userRepo;
        private readonly IBorrowRepository _borrowRepo;
        private readonly IReservationRepository _reservationRepo;

        public DashboardService(IBookRepository bookRepo, IUserRepository userRepo,
            IBorrowRepository borrowRepo, IReservationRepository reservationRepo)
        {
            _bookRepo = bookRepo;
            _userRepo = userRepo;
            _borrowRepo = borrowRepo;
            _reservationRepo = reservationRepo;
        }

        public async Task<DashboardStatsDto> GetStatsAsync()
        {
            var books = await _bookRepo.GetAllAsync();
            var users = await _userRepo.GetAllAsync();
            var allBorrows = await _borrowRepo.GetAllAsync();
            var activeBorrows = await _borrowRepo.GetAllActiveAsync();
            var activeReservations = await _reservationRepo.GetAllActiveAsync();

            var now = DateTime.UtcNow;
            var overdue = activeBorrows.Where(b => b.DueDate < now).ToList();
            
            var totalFinesCollected = allBorrows.Where(b => b.IsReturned).Sum(b => b.Fine);
            var totalFinesPending = overdue.Sum(b => BorrowService.CalculateFine(b.DueDate, now));

            return new DashboardStatsDto
            {
                TotalBooks = books.Count,
                TotalCopies = books.Sum(b => b.TotalCopies),
                AvailableCopies = books.Sum(b => b.AvailableCopies),
                BorrowedCopies = books.Sum(b => b.TotalCopies - b.AvailableCopies),
                ActiveBorrows = activeBorrows.Count,
                OverdueBorrows = overdue.Count,
                TotalUsers = users.Count,
                TotalFinesCollected = totalFinesCollected,
                TotalFinesPending = totalFinesPending,
                ActiveReservations = activeReservations.Count
            };
        }
    }
}
