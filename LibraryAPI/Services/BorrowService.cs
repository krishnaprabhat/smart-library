using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public class BorrowService
    {
        private readonly IBorrowRepository _borrowRepo;
        private readonly IBookRepository _bookRepo;
        private const int BorrowDays = 7;
        private const decimal FinePerDay = 10m;

        public BorrowService(IBorrowRepository borrowRepo, IBookRepository bookRepo)
        {
            _borrowRepo = borrowRepo;
            _bookRepo = bookRepo;
        }

        public async Task<(bool Success, string Message, BorrowResponseDto? Record)> BorrowAsync(int userId, BorrowRequestDto dto)
        {
            var book = await _bookRepo.GetByIdAsync(dto.BookId);
            if (book == null) return (false, "Book not found.", null);
            if (book.AvailableCopies <= 0) return (false, "No copies available for this book.", null);

            book.AvailableCopies--;
            await _bookRepo.UpdateAsync(book);
            await _bookRepo.SaveChangesAsync();

            var record = new BorrowRecord
            {
                UserId = userId,
                BookId = dto.BookId,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(BorrowDays),
                IsReturned = false,
                Fine = 0
            };

            var created = await _borrowRepo.AddAsync(record);
            var full = await _borrowRepo.GetByIdAsync(created.Id);
            return (true, "Book borrowed successfully.", MapToDto(full!));
        }

        public async Task<(bool Success, string Message, BorrowResponseDto? Record)> ReturnAsync(int borrowId, int userId, bool adminOverride = false)
        {
            var record = await _borrowRepo.GetByIdAsync(borrowId);
            if (record == null) return (false, "Borrow record not found.", null);
            if (!adminOverride && record.UserId != userId) return (false, "Unauthorized.", null);
            if (record.IsReturned) return (false, "Book already returned.", null);

            var returnDate = DateTime.UtcNow;
            record.ReturnDate = returnDate;
            record.IsReturned = true;
            record.Fine = CalculateFine(record.DueDate, returnDate);

            record.Book.AvailableCopies++;
            await _borrowRepo.SaveChangesAsync();

            return (true, "Book returned successfully.", MapToDto(record));
        }

        public async Task<List<BorrowResponseDto>> GetMyBorrowsAsync(int userId)
        {
            var records = await _borrowRepo.GetByUserIdAsync(userId);
            return records.Select(MapToDto).ToList();
        }

        public async Task<List<BorrowResponseDto>> GetAllActiveBorrowsAsync()
        {
            var records = await _borrowRepo.GetAllActiveAsync();
            return records.Select(MapToDto).ToList();
        }

        public static decimal CalculateFine(DateTime dueDate, DateTime returnDate)
        {
            if (returnDate <= dueDate) return 0;
            var lateDays = (int)(returnDate - dueDate).TotalDays;
            return lateDays * FinePerDay;
        }

        private static BorrowResponseDto MapToDto(BorrowRecord r)
        {
            var now = DateTime.UtcNow;
            return new BorrowResponseDto
            {
                Id = r.Id,
                BookId = r.BookId,
                BookTitle = r.Book?.Title ?? "",
                BookAuthor = r.Book?.Author ?? "",
                StudentName = r.User?.Name ?? "",
                StudentId = r.User?.StudentId ?? "",
                BorrowDate = r.BorrowDate,
                DueDate = r.DueDate,
                ReturnDate = r.ReturnDate,
                Fine = r.IsReturned ? r.Fine : CalculateFine(r.DueDate, now),
                IsReturned = r.IsReturned,
                IsOverdue = !r.IsReturned && now > r.DueDate
            };
        }
    }
}
