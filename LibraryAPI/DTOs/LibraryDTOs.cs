namespace LibraryAPI.DTOs
{
    // ── Auth ──────────────────────────────────────────────────────────────────
    public class LoginDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterDto
    {
        public string Name { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    // ── Users (Admin) ─────────────────────────────────────────────────────────
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int BorrowCount { get; set; }
    }

    public class UpdateUserRoleDto
    {
        public string Role { get; set; } = string.Empty;
    }

    public class UpdateProfileDto
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

    // ── Books ─────────────────────────────────────────────────────────────────
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
    }

    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalCopies { get; set; }
    }

    public class UpdateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
    }

    // ── Borrow / Return ───────────────────────────────────────────────────────
    public class BorrowRequestDto
    {
        public int BookId { get; set; }
    }

    public class BorrowResponseDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookAuthor { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal Fine { get; set; }
        public bool IsReturned { get; set; }
        public bool IsOverdue { get; set; }
    }

    // ── Reservations ──────────────────────────────────────────────────────────
    public class CreateReservationDto
    {
        public int BookId { get; set; }
    }

    public class ReservationDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string BookAuthor { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public DateTime ReservedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsFulfilled { get; set; }
        public bool IsCancelled { get; set; }
    }

    // ── Dashboard ─────────────────────────────────────────────────────────────
    public class DashboardStatsDto
    {
        public int TotalBooks { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public int BorrowedCopies { get; set; }
        public int ActiveBorrows { get; set; }
        public int OverdueBorrows { get; set; }
        public int TotalUsers { get; set; }
        public decimal TotalFinesCollected { get; set; }
        public decimal TotalFinesPending { get; set; }
        public int ActiveReservations { get; set; }
    }
}
