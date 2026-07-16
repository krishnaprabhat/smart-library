namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty; // CSE, ECE, MECH, CIVIL, EEE
        public string Type { get; set; } = string.Empty;   // Textbook, Reference, Novel
        public string ISBN { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
