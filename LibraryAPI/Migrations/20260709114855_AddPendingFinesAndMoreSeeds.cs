using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPendingFinesAndMoreSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "AvailableCopies", "Branch", "Category", "Description", "ISBN", "Publisher", "Title", "TotalCopies", "Type" },
                values: new object[,]
                {
                    { 31, "Cal Newport", 4, "GENERAL", "Self-Help", "", "978-1455586691", "Grand Central Publishing", "Deep Work", 4, "Novel" },
                    { 32, "Mary Shelley", 3, "GENERAL", "Classic Novel", "", "978-0141439471", "Penguin Classics", "Frankenstein", 3, "Novel" },
                    { 33, "Aldous Huxley", 4, "GENERAL", "Dystopian Novel", "", "978-0060850524", "Harper Perennial", "Brave New World", 4, "Novel" },
                    { 34, "Alex Michaelides", 5, "GENERAL", "Psychological Thriller", "", "978-1250301697", "Celadon Books", "The Silent Patient", 5, "Novel" },
                    { 35, "Eric Matthes", 5, "CSE", "Python Programming", "", "978-1593279288", "No Starch Press", "Python Crash Course", 5, "Textbook" },
                    { 36, "Kyle Simpson", 4, "CSE", "JavaScript", "", "978-1092478533", "Independently Published", "You Don't Know JS Yet", 4, "Reference" },
                    { 37, "Adel S. Sedra", 3, "ECE", "Electronics", "", "978-0190853464", "Oxford University Press", "Microelectronic Circuits", 3, "Textbook" },
                    { 38, "Richard G. Budynas", 3, "MECH", "Mechanical Design", "", "978-0073398204", "McGraw-Hill Education", "Mechanical Engineering Design", 3, "Textbook" },
                    { 39, "J.R.R. Tolkien & David Wenzel", 3, "GENERAL", "Graphic Novel", "", "978-0345445261", "Del Rey", "The Hobbit: Graphic Novel", 3, "Novel" },
                    { 40, "John E. Hopcroft", 3, "CSE", "Formal Languages", "", "978-0201441246", "Addison-Wesley", "Automata Theory", 3, "Textbook" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 9, 11, 48, 52, 630, DateTimeKind.Utc).AddTicks(1577), "$2a$11$/bLG8bAFBUqKUAK1/2aftejg.1Mud4w3kOeBLqVcqs4WmwH.Zfkha" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 9, 11, 48, 52, 899, DateTimeKind.Utc).AddTicks(6773), "$2a$11$rpiqpfOMHdo.UYi4hfo5DOCvJZBlG2YyKRFzDRt8UPGvt4xAn4Ql." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 9, 11, 48, 53, 172, DateTimeKind.Utc).AddTicks(9874), "$2a$11$mtsDQHz1YkU5/ixvxNQXTO/OiCmoRXDBuGCoU/sSkHPJe.c5hSK5S" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 9, 11, 48, 53, 429, DateTimeKind.Utc).AddTicks(6159), "$2a$11$LxHg45IFudOML15cVwLpbugVn3W6yGtkg5pQWL.90qERM29pZs6KK" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 7, 9, 11, 48, 53, 737, DateTimeKind.Utc).AddTicks(9996), "$2a$11$jta99Q0kelfr1y0w6vXJ9u229iTaaM7zbUnrc/83wEEvDiowjyc72" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 40);

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
    }
}
