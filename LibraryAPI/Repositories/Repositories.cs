using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;
        public UserRepository(LibraryDbContext context) => _context = context;

        public async Task<User?> GetByStudentIdAsync(string studentId) =>
            await _context.Users.FirstOrDefaultAsync(u => u.StudentId == studentId);

        public async Task<User?> GetByIdAsync(int id) =>
            await _context.Users.FindAsync(id);

        public async Task<List<User>> GetAllAsync() =>
            await _context.Users.OrderBy(u => u.Role).ThenBy(u => u.Name).ToListAsync();

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public Task UpdateAsync(User user) { _context.Users.Update(user); return Task.CompletedTask; }

        public Task DeleteAsync(User user) { _context.Users.Remove(user); return Task.CompletedTask; }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }

    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context) => _context = context;

        public async Task<List<Book>> GetAllAsync(string? search = null, string? branch = null, string? category = null)
        {
            var query = _context.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(b => b.Title.ToLower().Contains(search.ToLower()) ||
                                         b.Author.ToLower().Contains(search.ToLower()) ||
                                         b.ISBN.ToLower().Contains(search.ToLower()));
            if (!string.IsNullOrWhiteSpace(branch))
                query = query.Where(b => b.Branch == branch);
            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(b => b.Category == category);
            return await query.OrderBy(b => b.Title).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id) => await _context.Books.FindAsync(id);

        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public Task UpdateAsync(Book book) { _context.Books.Update(book); return Task.CompletedTask; }

        public Task DeleteAsync(Book book) { _context.Books.Remove(book); return Task.CompletedTask; }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }

    public class BorrowRepository : IBorrowRepository
    {
        private readonly LibraryDbContext _context;
        public BorrowRepository(LibraryDbContext context) => _context = context;

        public async Task<List<BorrowRecord>> GetByUserIdAsync(int userId) =>
            await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BorrowDate)
                .ToListAsync();

        public async Task<List<BorrowRecord>> GetAllActiveAsync() =>
            await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => !b.IsReturned)
                .OrderByDescending(b => b.BorrowDate)
                .ToListAsync();

        public async Task<List<BorrowRecord>> GetAllAsync() =>
            await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .OrderByDescending(b => b.BorrowDate)
                .ToListAsync();

        public async Task<BorrowRecord?> GetByIdAsync(int id) =>
            await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<BorrowRecord> AddAsync(BorrowRecord record)
        {
            _context.BorrowRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }

    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryDbContext _context;
        public ReservationRepository(LibraryDbContext context) => _context = context;

        public async Task<List<Reservation>> GetByUserIdAsync(int userId) =>
            await _context.Reservations
                .Include(r => r.Book)
                .Include(r => r.User)
                .Where(r => r.UserId == userId && !r.IsCancelled)
                .OrderByDescending(r => r.ReservedAt)
                .ToListAsync();

        public async Task<List<Reservation>> GetAllActiveAsync() =>
            await _context.Reservations
                .Include(r => r.Book)
                .Include(r => r.User)
                .Where(r => !r.IsCancelled && !r.IsFulfilled && r.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(r => r.ReservedAt)
                .ToListAsync();

        public async Task<Reservation?> GetByIdAsync(int id) =>
            await _context.Reservations
                .Include(r => r.Book)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<Reservation> AddAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
