using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByStudentIdAsync(string studentId);
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
    }

    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(string? search = null, string? branch = null, string? category = null);
        Task<Book?> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task SaveChangesAsync();
    }

    public interface IBorrowRepository
    {
        Task<List<BorrowRecord>> GetByUserIdAsync(int userId);
        Task<List<BorrowRecord>> GetAllActiveAsync();
        Task<List<BorrowRecord>> GetAllAsync();
        Task<BorrowRecord?> GetByIdAsync(int id);
        Task<BorrowRecord> AddAsync(BorrowRecord record);
        Task SaveChangesAsync();
    }

    public interface IReservationRepository
    {
        Task<List<Reservation>> GetByUserIdAsync(int userId);
        Task<List<Reservation>> GetAllActiveAsync();
        Task<Reservation?> GetByIdAsync(int id);
        Task<Reservation> AddAsync(Reservation reservation);
        Task SaveChangesAsync();
    }
}
