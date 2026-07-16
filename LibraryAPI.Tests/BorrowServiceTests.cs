using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using LibraryAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Tests
{
    public class BorrowServiceTests
    {
        private LibraryDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            // Do NOT call EnsureCreated() — it would run OnModelCreating seed data
            // which conflicts with our test data (same IDs).
            return new LibraryDbContext(options);
        }

        private async Task SeedData(LibraryDbContext ctx)
        {
            ctx.Users.Add(new User
            {
                Id = 100, Name = "Test Student", StudentId = "s1@test.com",
                PasswordHash = "hash", Role = UserRole.Student
            });
            ctx.Books.Add(new Book
            {
                Id = 200, Title = "Test Book", Author = "Author",
                Branch = "CSE", Type = "Textbook", TotalCopies = 2, AvailableCopies = 2
            });
            await ctx.SaveChangesAsync();
        }

        [Fact]
        public async Task BorrowBook_DecreasesAvailableCopies()
        {
            using var ctx = CreateContext("BorrowTest_Decrease");
            await SeedData(ctx);

            var borrowRepo = new BorrowRepository(ctx);
            var bookRepo = new BookRepository(ctx);
            var svc = new BorrowService(borrowRepo, bookRepo);

            var (success, _, _) = await svc.BorrowAsync(100, new BorrowRequestDto { BookId = 200 });

            Assert.True(success);
            var book = await ctx.Books.FindAsync(200);
            Assert.Equal(1, book!.AvailableCopies);
        }

        [Fact]
        public async Task ReturnBook_IncreasesAvailableCopies()
        {
            using var ctx = CreateContext("BorrowTest_Return");
            await SeedData(ctx);

            var borrowRepo = new BorrowRepository(ctx);
            var bookRepo = new BookRepository(ctx);
            var svc = new BorrowService(borrowRepo, bookRepo);

            var (_, _, borrowRecord) = await svc.BorrowAsync(100, new BorrowRequestDto { BookId = 200 });
            Assert.NotNull(borrowRecord);

            var (success, _, _) = await svc.ReturnAsync(borrowRecord!.Id, 100);

            Assert.True(success);
            var book = await ctx.Books.FindAsync(200);
            Assert.Equal(2, book!.AvailableCopies);
        }

        [Fact]
        public async Task BorrowBook_WhenNoCopiesAvailable_ReturnsFail()
        {
            using var ctx = CreateContext("BorrowTest_NoCopies");
            ctx.Users.Add(new User { Id = 100, Name = "Student", StudentId = "s@t.com", PasswordHash = "h", Role = UserRole.Student });
            ctx.Books.Add(new Book { Id = 200, Title = "Full Book", Author = "A", Branch = "CSE", Type = "T", TotalCopies = 1, AvailableCopies = 0 });
            await ctx.SaveChangesAsync();

            var borrowRepo = new BorrowRepository(ctx);
            var bookRepo = new BookRepository(ctx);
            var svc = new BorrowService(borrowRepo, bookRepo);

            var (success, message, _) = await svc.BorrowAsync(100, new BorrowRequestDto { BookId = 200 });

            Assert.False(success);
            Assert.Contains("No copies available", message);
        }

        [Fact]
        public async Task ReturnBook_AlreadyReturned_ReturnsFail()
        {
            using var ctx = CreateContext("BorrowTest_AlreadyReturned");
            await SeedData(ctx);

            var borrowRepo = new BorrowRepository(ctx);
            var bookRepo = new BookRepository(ctx);
            var svc = new BorrowService(borrowRepo, bookRepo);

            var (_, _, borrowRecord) = await svc.BorrowAsync(100, new BorrowRequestDto { BookId = 200 });
            await svc.ReturnAsync(borrowRecord!.Id, 100);

            var (success, message, _) = await svc.ReturnAsync(borrowRecord.Id, 100);

            Assert.False(success);
            Assert.Contains("already returned", message);
        }
    }
}
