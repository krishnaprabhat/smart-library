using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreCatalogAndFines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "AvailableCopies", "Branch", "Category", "Description", "ISBN", "Publisher", "Title", "TotalCopies", "Type" },
                values: new object[,]
                {
                    { 19, "F. Scott Fitzgerald", 4, "GENERAL", "Classic", "", "978-0743273565", "Scribner", "The Great Gatsby", 4, "Novel" },
                    { 20, "Harper Lee", 3, "GENERAL", "Classic", "", "978-0061120084", "Harper", "To Kill a Mockingbird", 3, "Novel" },
                    { 21, "George Orwell", 5, "GENERAL", "Dystopian", "", "978-0452284234", "Signet", "1984", 5, "Novel" },
                    { 22, "J.R.R. Tolkien", 4, "GENERAL", "Fantasy", "", "978-0547928227", "Mariner", "The Hobbit", 4, "Novel" },
                    { 23, "Condé Nast", 5, "CSE", "Technology", "", "MAG-WIRED-01", "Condé Nast", "Wired", 5, "Magazine" },
                    { 24, "NG Society", 6, "GENERAL", "Science", "", "MAG-NATGEO-02", "NG Partners", "National Geographic", 6, "Magazine" },
                    { 25, "Time USA", 4, "GENERAL", "News", "", "MAG-TIME-03", "Time USA", "Time Magazine", 4, "Magazine" },
                    { 26, "Forbes Media", 3, "GENERAL", "Business", "", "MAG-FORBES-04", "Forbes Media", "Forbes", 3, "Magazine" },
                    { 27, "Springer Nature", 3, "ECE", "Science", "", "MAG-SCIAM-05", "Springer", "Scientific American", 3, "Magazine" },
                    { 28, "Condé Nast", 4, "GENERAL", "Fashion", "", "MAG-VOGUE-06", "Condé Nast", "Vogue", 4, "Magazine" },
                    { 29, "James Clear", 5, "GENERAL", "Self-Help", "", "978-0735211292", "Avery", "Atomic Habits", 5, "Novel" },
                    { 30, "Frank Herbert", 4, "GENERAL", "Sci-Fi", "", "978-0441172719", "Ace", "Dune", 4, "Novel" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 9, 9, 55, 21, 587, DateTimeKind.Utc).AddTicks(1530), "$2a$11$yQPEhNLd9IR0VIoRl8.MluvHl1tYzeivJqMG4nb8kR9HML5WY5XWi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 9, 9, 55, 21, 895, DateTimeKind.Utc).AddTicks(9106), "$2a$11$BoFEQzVTKYYr3kC2.nDHQOcBiu26S6epC.DMWGkCXFfeDIs9IXn.2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 9, 9, 55, 22, 164, DateTimeKind.Utc).AddTicks(4458), "$2a$11$grlhMXuxL1VqCdSdUDtDt.NRsnde1JF0BLdL2lwer68Z19nrZEdra" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 9, 9, 55, 22, 429, DateTimeKind.Utc).AddTicks(9086), "$2a$11$09/oTockRAZZtI.U4r3dpuxXIEiTTH0pvde/lMXTqGW9ObRCfQJLG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 9, 9, 55, 22, 708, DateTimeKind.Utc).AddTicks(8282), "$2a$11$fk6x9wOlT59FHkPSte378O6AD3s8hbOLSUUmncsYJZyehCqGZdCb6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 7, 10, 32, 26, 474, DateTimeKind.Utc).AddTicks(6181), "$2a$11$C20r6I.rKl6XqivfYBV0regebHHnAAewuMsnChmpr1TC3U3TbR3SG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 7, 10, 32, 26, 701, DateTimeKind.Utc).AddTicks(7109), "$2a$11$hvTNGHGW0y9HWHTWilE.wObGiAD/EKq4NyIQg2/Ps/Aw7dYn9fO6S" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 7, 10, 32, 26, 863, DateTimeKind.Utc).AddTicks(9217), "$2a$11$NfYqySiZeH/s3ko8Mg1il.UJ4Q8i6BM9mX7NGBfFOEL/4agYn65nO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 7, 10, 32, 27, 23, DateTimeKind.Utc).AddTicks(1191), "$2a$11$3jRfuYURdy9y13kHoUgYZuPxqd1ctScAKk3T1QdeGOlroLGiRB/hW" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 7, 10, 32, 27, 199, DateTimeKind.Utc).AddTicks(7802), "$2a$11$w.FnkBs59Z.iXIW1nwBuh.OuOykTKKWMTHf9fsbhOw1wfrQG/6Ih." });
        }
    }
}
