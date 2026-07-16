namespace LibraryAPI.Models
{
    public enum UserRole { Student, Admin, Librarian }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty; // email used as login
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Student;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
