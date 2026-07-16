using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision
            modelBuilder.Entity<BorrowRecord>()
                .Property(b => b.Fine)
                .HasPrecision(10, 2);

            // Seed Admin user (password: Admin@123)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1, Name = "Library Admin", StudentId = "admin@library.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = UserRole.Admin, Department = "Administration"
                },
                new User
                {
                    Id = 2, Name = "Arjun Kumar", StudentId = "student1@library.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                    Role = UserRole.Student, Department = "CSE"
                },
                new User
                {
                    Id = 3, Name = "Priya Sharma", StudentId = "student2@library.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                    Role = UserRole.Student, Department = "ECE"
                },
                new User
                {
                    Id = 4, Name = "Rahul Verma", StudentId = "student3@library.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
                    Role = UserRole.Student, Department = "MECH"
                },
                new User
                {
                    Id = 5, Name = "Head Librarian", StudentId = "librarian@library.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Librarian@123"),
                    Role = UserRole.Librarian, Department = "Library"
                }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1,  Title = "Operating System Concepts",              Author = "Silberschatz",     Branch = "CSE",   Type = "Textbook",  ISBN = "978-1-118-06333-0", Category = "Systems",      Publisher = "Wiley",          TotalCopies = 5, AvailableCopies = 5 },
                new Book { Id = 2,  Title = "Introduction to Algorithms",              Author = "CLRS",             Branch = "CSE",   Type = "Textbook",  ISBN = "978-0-262-03384-8", Category = "Algorithms",   Publisher = "MIT Press",      TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 3,  Title = "Design Patterns",                         Author = "Gang of Four",     Branch = "CSE",   Type = "Reference", ISBN = "978-0-201-63361-0", Category = "Software Eng", Publisher = "Addison-Wesley", TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 4,  Title = "Clean Code",                              Author = "Robert C. Martin", Branch = "CSE",   Type = "Reference", ISBN = "978-0-13-235088-4", Category = "Software Eng", Publisher = "Pearson",        TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 5,  Title = "Electronic Devices and Circuits",         Author = "Boylestad",        Branch = "ECE",   Type = "Textbook",  ISBN = "978-0-13-272971-9", Category = "Electronics",  Publisher = "Pearson",        TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 6,  Title = "Signals and Systems",                     Author = "Oppenheim",        Branch = "ECE",   Type = "Textbook",  ISBN = "978-0-13-814587-3", Category = "Signal Proc",  Publisher = "Pearson",        TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 7,  Title = "Digital Communications",                  Author = "Proakis",          Branch = "ECE",   Type = "Reference", ISBN = "978-0-07-295716-7", Category = "Communications", Publisher = "McGraw-Hill",  TotalCopies = 2, AvailableCopies = 2 },
                new Book { Id = 8,  Title = "Engineering Mechanics",                   Author = "Hibbeler",         Branch = "MECH",  Type = "Textbook",  ISBN = "978-0-13-441189-3", Category = "Mechanics",    Publisher = "Pearson",        TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 9,  Title = "Thermodynamics: An Engineering Approach", Author = "Cengel",           Branch = "MECH",  Type = "Textbook",  ISBN = "978-0-07-339817-4", Category = "Thermodynamics", Publisher = "McGraw-Hill",  TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 10, Title = "Fluid Mechanics",                         Author = "Frank White",      Branch = "MECH",  Type = "Reference", ISBN = "978-0-07-352934-9", Category = "Fluid Mech",   Publisher = "McGraw-Hill",    TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 11, Title = "Structural Analysis",                     Author = "RC Hibbeler",      Branch = "CIVIL", Type = "Textbook",  ISBN = "978-0-13-479146-4", Category = "Structures",   Publisher = "Pearson",        TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 12, Title = "Surveying",                               Author = "BC Punmia",        Branch = "CIVIL", Type = "Textbook",  ISBN = "978-81-7008-354-2", Category = "Surveying",    Publisher = "Laxmi Pubs",     TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 13, Title = "Soil Mechanics",                          Author = "Braja M. Das",     Branch = "CIVIL", Type = "Reference", ISBN = "978-1-305-97093-1", Category = "Geotechnical", Publisher = "Cengage",        TotalCopies = 2, AvailableCopies = 2 },
                new Book { Id = 14, Title = "Power Systems Analysis",                  Author = "Stevenson",        Branch = "EEE",   Type = "Textbook",  ISBN = "978-0-07-061293-0", Category = "Power Sys",    Publisher = "McGraw-Hill",    TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 15, Title = "Electrical Machines",                     Author = "Chapman",          Branch = "EEE",   Type = "Textbook",  ISBN = "978-0-07-338090-2", Category = "Machines",     Publisher = "McGraw-Hill",    TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 16, Title = "Computer Networks",                       Author = "Tanenbaum",        Branch = "CSE",   Type = "Textbook",  ISBN = "978-0-13-212695-3", Category = "Networks",     Publisher = "Pearson",        TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 17, Title = "Database System Concepts",                Author = "Silberschatz",     Branch = "CSE",   Type = "Textbook",  ISBN = "978-0-07-352332-3", Category = "Databases",    Publisher = "McGraw-Hill",    TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 18, Title = "Artificial Intelligence: A Modern Approach", Author = "Russell & Norvig", Branch = "CSE", Type = "Reference", ISBN = "978-0-13-468599-1", Category = "AI/ML",       Publisher = "Pearson",        TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 19, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Branch = "GENERAL", Type = "Novel", ISBN = "978-0743273565", Category = "Classic", Publisher = "Scribner", TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 20, Title = "To Kill a Mockingbird", Author = "Harper Lee", Branch = "GENERAL", Type = "Novel", ISBN = "978-0061120084", Category = "Classic", Publisher = "Harper", TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 21, Title = "1984", Author = "George Orwell", Branch = "GENERAL", Type = "Novel", ISBN = "978-0452284234", Category = "Dystopian", Publisher = "Signet", TotalCopies = 5, AvailableCopies = 5 },
                new Book { Id = 22, Title = "The Hobbit", Author = "J.R.R. Tolkien", Branch = "GENERAL", Type = "Novel", ISBN = "978-0547928227", Category = "Fantasy", Publisher = "Mariner", TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 23, Title = "Wired", Author = "Condé Nast", Branch = "CSE", Type = "Magazine", ISBN = "MAG-WIRED-01", Category = "Technology", Publisher = "Condé Nast", TotalCopies = 5, AvailableCopies = 5 },
                new Book { Id = 24, Title = "National Geographic", Author = "NG Society", Branch = "GENERAL", Type = "Magazine", ISBN = "MAG-NATGEO-02", Category = "Science", Publisher = "NG Partners", TotalCopies = 6, AvailableCopies = 6 },
                new Book { Id = 25, Title = "Time Magazine", Author = "Time USA", Branch = "GENERAL", Type = "Magazine", ISBN = "MAG-TIME-03", Category = "News", Publisher = "Time USA", TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 26, Title = "Forbes", Author = "Forbes Media", Branch = "GENERAL", Type = "Magazine", ISBN = "MAG-FORBES-04", Category = "Business", Publisher = "Forbes Media", TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 27, Title = "Scientific American", Author = "Springer Nature", Branch = "ECE", Type = "Magazine", ISBN = "MAG-SCIAM-05", Category = "Science", Publisher = "Springer", TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 28, Title = "Vogue", Author = "Condé Nast", Branch = "GENERAL", Type = "Magazine", ISBN = "MAG-VOGUE-06", Category = "Fashion", Publisher = "Condé Nast", TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 29, Title = "Atomic Habits", Author = "James Clear", Branch = "GENERAL", Type = "Novel", ISBN = "978-0735211292", Category = "Self-Help", Publisher = "Avery", TotalCopies = 5, AvailableCopies = 5 },
                new Book { Id = 30, Title = "Dune", Author = "Frank Herbert", Branch = "GENERAL", Type = "Novel", ISBN = "978-0441172719", Category = "Sci-Fi", Publisher = "Ace", TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 31, Title = "Deep Work", Author = "Cal Newport", Branch = "GENERAL", Type = "Novel", ISBN = "978-1455586691", Category = "Self-Help", Publisher = "Grand Central Publishing", TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 32, Title = "Frankenstein", Author = "Mary Shelley", Branch = "GENERAL", Type = "Novel", ISBN = "978-0141439471", Category = "Classic Novel", Publisher = "Penguin Classics", TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 33, Title = "Brave New World", Author = "Aldous Huxley", Branch = "GENERAL", Type = "Novel", ISBN = "978-0060850524", Category = "Dystopian Novel", Publisher = "Harper Perennial", TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 34, Title = "The Silent Patient", Author = "Alex Michaelides", Branch = "GENERAL", Type = "Novel", ISBN = "978-1250301697", Category = "Psychological Thriller", Publisher = "Celadon Books", TotalCopies = 5, AvailableCopies = 5 },
                new Book { Id = 35, Title = "Python Crash Course", Author = "Eric Matthes", Branch = "CSE", Type = "Textbook", ISBN = "978-1593279288", Category = "Python Programming", Publisher = "No Starch Press", TotalCopies = 5, AvailableCopies = 5 },
                new Book { Id = 36, Title = "You Don't Know JS Yet", Author = "Kyle Simpson", Branch = "CSE", Type = "Reference", ISBN = "978-1092478533", Category = "JavaScript", Publisher = "Independently Published", TotalCopies = 4, AvailableCopies = 4 },
                new Book { Id = 37, Title = "Microelectronic Circuits", Author = "Adel S. Sedra", Branch = "ECE", Type = "Textbook", ISBN = "978-0190853464", Category = "Electronics", Publisher = "Oxford University Press", TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 38, Title = "Mechanical Engineering Design", Author = "Richard G. Budynas", Branch = "MECH", Type = "Textbook", ISBN = "978-0073398204", Category = "Mechanical Design", Publisher = "McGraw-Hill Education", TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 39, Title = "The Hobbit: Graphic Novel", Author = "J.R.R. Tolkien & David Wenzel", Branch = "GENERAL", Type = "Novel", ISBN = "978-0345445261", Category = "Graphic Novel", Publisher = "Del Rey", TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 40, Title = "Automata Theory", Author = "John E. Hopcroft", Branch = "CSE", Type = "Textbook", ISBN = "978-0201441246", Category = "Formal Languages", Publisher = "Addison-Wesley", TotalCopies = 3, AvailableCopies = 3 }
            );
        }
    }
}
