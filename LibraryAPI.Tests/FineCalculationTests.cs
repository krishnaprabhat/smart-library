using LibraryAPI.Services;

namespace LibraryAPI.Tests
{
    public class FineCalculationTests
    {
        [Fact]
        public void ReturnedOnTime_Fine_ShouldBeZero()
        {
            var dueDate = new DateTime(2024, 1, 15);
            var returnDate = new DateTime(2024, 1, 15); // same day
            var fine = BorrowService.CalculateFine(dueDate, returnDate);
            Assert.Equal(0m, fine);
        }

        [Fact]
        public void ReturnedEarly_Fine_ShouldBeZero()
        {
            var dueDate = new DateTime(2024, 1, 15);
            var returnDate = new DateTime(2024, 1, 12); // 3 days early
            var fine = BorrowService.CalculateFine(dueDate, returnDate);
            Assert.Equal(0m, fine);
        }

        [Fact]
        public void ReturnedThreeDaysLate_Fine_ShouldBe30()
        {
            var dueDate = new DateTime(2024, 1, 15);
            var returnDate = new DateTime(2024, 1, 18); // 3 days late
            var fine = BorrowService.CalculateFine(dueDate, returnDate);
            Assert.Equal(30m, fine);
        }

        [Fact]
        public void ReturnedTenDaysLate_Fine_ShouldBe100()
        {
            var dueDate = new DateTime(2024, 1, 15);
            var returnDate = new DateTime(2024, 1, 25); // 10 days late
            var fine = BorrowService.CalculateFine(dueDate, returnDate);
            Assert.Equal(100m, fine);
        }

        [Fact]
        public void ReturnedOneDayLate_Fine_ShouldBe10()
        {
            var dueDate = new DateTime(2024, 3, 1);
            var returnDate = new DateTime(2024, 3, 2); // 1 day late
            var fine = BorrowService.CalculateFine(dueDate, returnDate);
            Assert.Equal(10m, fine);
        }
    }
}
