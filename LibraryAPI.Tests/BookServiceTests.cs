using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using LibraryAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Tests
{
    public class BookServiceTests
    {
        private LibraryDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            // Do NOT call EnsureCreated() to keep DB empty for predictable test data
            return new LibraryDbContext(options);
        }

        private async Task SeedBooks(LibraryDbContext ctx)
        {
            ctx.Books.AddRange(
                new Book { Id = 100, Title = "Algorithms in C", Author = "Sedgewick", Branch = "CSE", Type = "Textbook", TotalCopies = 3, AvailableCopies = 3 },
                new Book { Id = 101, Title = "Digital Systems", Author = "Tocci", Branch = "ECE", Type = "Textbook", TotalCopies = 2, AvailableCopies = 2 },
                new Book { Id = 102, Title = "C++ Programming", Author = "Stroustrup", Branch = "CSE", Type = "Reference", TotalCopies = 4, AvailableCopies = 4 }
            );
            await ctx.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAll_FilterByBranch_ReturnsOnlyCSEBooks()
        {
            using var ctx = CreateContext("BookTest_Filter");
            await SeedBooks(ctx);

            var svc = new BookService(new BookRepository(ctx));
            var result = await svc.GetAllAsync(null, "CSE");

            Assert.Equal(2, result.Count);
            Assert.All(result, b => Assert.Equal("CSE", b.Branch));
        }

        [Fact]
        public async Task GetAll_SearchByTitle_ReturnsCaseInsensitiveMatch()
        {
            using var ctx = CreateContext("BookTest_Search");
            await SeedBooks(ctx);

            var svc = new BookService(new BookRepository(ctx));
            var result = await svc.GetAllAsync("algorithms", null);

            Assert.Single(result);
            Assert.Equal("Algorithms in C", result[0].Title);
        }

        [Fact]
        public async Task CreateBook_SetsAvailableCopiesToTotalCopies()
        {
            using var ctx = CreateContext("BookTest_Create");
            var svc = new BookService(new BookRepository(ctx));

            var dto = new LibraryAPI.DTOs.CreateBookDto
            {
                Title = "New Book", Author = "Author", Branch = "MECH",
                Type = "Textbook", TotalCopies = 5
            };

            var created = await svc.CreateAsync(dto);

            Assert.Equal(5, created.TotalCopies);
            Assert.Equal(5, created.AvailableCopies);
        }
    }
}
