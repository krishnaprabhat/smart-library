using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepo;

        public BookService(IBookRepository bookRepo) => _bookRepo = bookRepo;

        public async Task<List<BookDto>> GetAllAsync(string? search, string? branch)
        {
            var books = await _bookRepo.GetAllAsync(search, branch);
            return books.Select(MapToDto).ToList();
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            return book == null ? null : MapToDto(book);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Branch = dto.Branch,
                Type = dto.Type,
                ISBN = dto.ISBN,
                Category = dto.Category,
                Publisher = dto.Publisher,
                Description = dto.Description,
                TotalCopies = dto.TotalCopies,
                AvailableCopies = dto.TotalCopies
            };
            var created = await _bookRepo.AddAsync(book);
            return MapToDto(created);
        }

        public async Task<bool> UpdateAsync(int id, UpdateBookDto dto)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) return false;

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Branch = dto.Branch;
            book.Type = dto.Type;
            book.ISBN = dto.ISBN;
            book.Category = dto.Category;
            book.Publisher = dto.Publisher;
            book.Description = dto.Description;
            book.TotalCopies = dto.TotalCopies;
            book.AvailableCopies = dto.AvailableCopies;

            await _bookRepo.UpdateAsync(book);
            await _bookRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) return false;

            await _bookRepo.DeleteAsync(book);
            await _bookRepo.SaveChangesAsync();
            return true;
        }

        private static BookDto MapToDto(Book b) => new()
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            Branch = b.Branch,
            Type = b.Type,
            ISBN = b.ISBN,
            Category = b.Category,
            Publisher = b.Publisher,
            Description = b.Description,
            TotalCopies = b.TotalCopies,
            AvailableCopies = b.AvailableCopies
        };
    }
}
