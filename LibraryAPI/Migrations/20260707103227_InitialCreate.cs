using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalCopies = table.Column<int>(type: "int", nullable: false),
                    AvailableCopies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BorrowRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fine = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowRecords_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ReservedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFulfilled = table.Column<bool>(type: "bit", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "AvailableCopies", "Branch", "Category", "Description", "ISBN", "Publisher", "Title", "TotalCopies", "Type" },
                values: new object[,]
                {
                    { 1, "Silberschatz", 5, "CSE", "Systems", "", "978-1-118-06333-0", "Wiley", "Operating System Concepts", 5, "Textbook" },
                    { 2, "CLRS", 4, "CSE", "Algorithms", "", "978-0-262-03384-8", "MIT Press", "Introduction to Algorithms", 4, "Textbook" },
                    { 3, "Gang of Four", 3, "CSE", "Software Eng", "", "978-0-201-63361-0", "Addison-Wesley", "Design Patterns", 3, "Reference" },
                    { 4, "Robert C. Martin", 3, "CSE", "Software Eng", "", "978-0-13-235088-4", "Pearson", "Clean Code", 3, "Reference" },
                    { 5, "Boylestad", 4, "ECE", "Electronics", "", "978-0-13-272971-9", "Pearson", "Electronic Devices and Circuits", 4, "Textbook" },
                    { 6, "Oppenheim", 3, "ECE", "Signal Proc", "", "978-0-13-814587-3", "Pearson", "Signals and Systems", 3, "Textbook" },
                    { 7, "Proakis", 2, "ECE", "Communications", "", "978-0-07-295716-7", "McGraw-Hill", "Digital Communications", 2, "Reference" },
                    { 8, "Hibbeler", 4, "MECH", "Mechanics", "", "978-0-13-441189-3", "Pearson", "Engineering Mechanics", 4, "Textbook" },
                    { 9, "Cengel", 3, "MECH", "Thermodynamics", "", "978-0-07-339817-4", "McGraw-Hill", "Thermodynamics: An Engineering Approach", 3, "Textbook" },
                    { 10, "Frank White", 3, "MECH", "Fluid Mech", "", "978-0-07-352934-9", "McGraw-Hill", "Fluid Mechanics", 3, "Reference" },
                    { 11, "RC Hibbeler", 4, "CIVIL", "Structures", "", "978-0-13-479146-4", "Pearson", "Structural Analysis", 4, "Textbook" },
                    { 12, "BC Punmia", 3, "CIVIL", "Surveying", "", "978-81-7008-354-2", "Laxmi Pubs", "Surveying", 3, "Textbook" },
                    { 13, "Braja M. Das", 2, "CIVIL", "Geotechnical", "", "978-1-305-97093-1", "Cengage", "Soil Mechanics", 2, "Reference" },
                    { 14, "Stevenson", 4, "EEE", "Power Sys", "", "978-0-07-061293-0", "McGraw-Hill", "Power Systems Analysis", 4, "Textbook" },
                    { 15, "Chapman", 3, "EEE", "Machines", "", "978-0-07-338090-2", "McGraw-Hill", "Electrical Machines", 3, "Textbook" },
                    { 16, "Tanenbaum", 3, "CSE", "Networks", "", "978-0-13-212695-3", "Pearson", "Computer Networks", 3, "Textbook" },
                    { 17, "Silberschatz", 4, "CSE", "Databases", "", "978-0-07-352332-3", "McGraw-Hill", "Database System Concepts", 4, "Textbook" },
                    { 18, "Russell & Norvig", 3, "CSE", "AI/ML", "", "978-0-13-468599-1", "Pearson", "Artificial Intelligence: A Modern Approach", 3, "Reference" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Department", "Name", "PasswordHash", "Phone", "Role", "StudentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 7, 10, 32, 26, 474, DateTimeKind.Utc).AddTicks(6181), "Administration", "Library Admin", "$2a$11$C20r6I.rKl6XqivfYBV0regebHHnAAewuMsnChmpr1TC3U3TbR3SG", "", 1, "admin@library.com" },
                    { 2, new DateTime(2026, 7, 7, 10, 32, 26, 701, DateTimeKind.Utc).AddTicks(7109), "CSE", "Arjun Kumar", "$2a$11$hvTNGHGW0y9HWHTWilE.wObGiAD/EKq4NyIQg2/Ps/Aw7dYn9fO6S", "", 0, "student1@library.com" },
                    { 3, new DateTime(2026, 7, 7, 10, 32, 26, 863, DateTimeKind.Utc).AddTicks(9217), "ECE", "Priya Sharma", "$2a$11$NfYqySiZeH/s3ko8Mg1il.UJ4Q8i6BM9mX7NGBfFOEL/4agYn65nO", "", 0, "student2@library.com" },
                    { 4, new DateTime(2026, 7, 7, 10, 32, 27, 23, DateTimeKind.Utc).AddTicks(1191), "MECH", "Rahul Verma", "$2a$11$3jRfuYURdy9y13kHoUgYZuPxqd1ctScAKk3T1QdeGOlroLGiRB/hW", "", 0, "student3@library.com" },
                    { 5, new DateTime(2026, 7, 7, 10, 32, 27, 199, DateTimeKind.Utc).AddTicks(7802), "Library", "Head Librarian", "$2a$11$w.FnkBs59Z.iXIW1nwBuh.OuOykTKKWMTHf9fsbhOw1wfrQG/6Ih.", "", 2, "librarian@library.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecords_BookId",
                table: "BorrowRecords",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecords_UserId",
                table: "BorrowRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BookId",
                table: "Reservations",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowRecords");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
