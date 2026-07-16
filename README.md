# 📚 Smart Campus Library Management System

A full-stack, real-time campus library management system built with **ASP.NET Core 8** (backend) and **Angular 17** (frontend), with SQL Server (LocalDB) as the database.

---

## 🏗️ Architecture

```
smart library/
├── LibraryAPI/              ← ASP.NET Core 8 Web API (backend)
├── LibraryAPI.Tests/        ← xUnit unit tests (12 tests, all passing)
├── library-frontend/        ← Angular 17 SPA (frontend)
└── SmartLibrary.sln         ← Visual Studio solution file
```

---

## ✨ Features

### Student Features
- 🔐 Secure JWT-based login & registration
- 📖 Browse full book catalog with search & branch filtering
- 📥 Borrow books (7-day due period)
- 📋 View current borrows + return history
- ↩️ Self-service book returns
- ⚠️ Real-time overdue & fine display (Rs.10/day)

### Admin / Librarian Features
- 📊 Real-time dashboard with stats (total books, borrows, overdue, fines)
- 📚 Full CRUD for books (add, edit, delete with ISBN/category/publisher)
- 👥 Manage users (view all, change roles, delete)
- 📤 Monitor all active borrows, process returns
- 💰 Automatic fine calculation on return

### System
- ✅ Repository pattern with clean separation of concerns
- ✅ JWT Bearer authentication with role-based authorization
- ✅ EF Core with LocalDB (SQL Server) + automatic migrations on startup
- ✅ 12 xUnit unit tests covering fine calculation, borrowing & book filtering
- ✅ CORS configured for Angular dev server (localhost:4200)
- ✅ Swagger/OpenAPI UI at `/swagger`

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [Node.js 18+](https://nodejs.org/) and npm
- SQL Server LocalDB (included with Visual Studio) **or** any SQL Server instance

### 1. Run the Backend API

```powershell
cd "smart library\LibraryAPI"
dotnet run
```

The API will start at `http://localhost:5247`.
Swagger UI: `http://localhost:5247/swagger`

> **Database is auto-created and seeded** on first run via EF Core migrations.

### 2. Run the Frontend

```powershell
cd "smart library\library-frontend"
npm install       # first time only
npm start
```

Open `http://localhost:4200` in your browser.

---

## 🔑 Default Login Credentials

| Role      | Email                         | Password      |
|-----------|-------------------------------|---------------|
| Admin     | admin@library.com             | Admin@123     |
| Librarian | librarian@library.com         | Librarian@123 |
| Student   | student1@library.com          | Student@123   |
| Student   | student2@library.com          | Student@123   |
| Student   | student3@library.com          | Student@123   |

---

## 🧪 Running Unit Tests

```powershell
cd "smart library"
dotnet test SmartLibrary.sln
```

**12 tests, all passing:**
- `FineCalculationTests` (5): zero fine, early return, 1/3/10 days late
- `BorrowServiceTests` (4): borrow decreases copies, return increases, no copies fail, double-return fail
- `BookServiceTests` (3): branch filter, case-insensitive search, auto-set available copies

---

## 🗄️ Database Configuration

**Default (LocalDB — no Docker needed):**
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SmartLibraryDB;Trusted_Connection=True;"
```

**Using Docker SQL Server** (change `DefaultConnection` in `appsettings.json`):
```json
"DefaultConnection": "Server=localhost,1433;Database=SmartLibraryDB;User Id=sa;Password=Library@Password123;TrustServerCertificate=True;"
```

---

## 📡 API Endpoints

| Method | Endpoint                      | Access        | Description                   |
|--------|-------------------------------|---------------|-------------------------------|
| POST   | /api/auth/login               | Public        | Login and get JWT token        |
| POST   | /api/auth/register            | Public        | Register new student account   |
| GET    | /api/books                    | Authenticated | List/search books              |
| POST   | /api/books                    | Admin         | Add new book                   |
| PUT    | /api/books/{id}               | Admin         | Update book details            |
| DELETE | /api/books/{id}               | Admin         | Delete book                    |
| POST   | /api/borrow                   | Authenticated | Borrow a book                  |
| POST   | /api/borrow/return/{id}       | Authenticated | Return a borrowed book         |
| GET    | /api/borrow/my                | Authenticated | Get my borrow history          |
| GET    | /api/borrow/all               | Admin/Librarian | All active borrows           |
| POST   | /api/reservations             | Authenticated | Reserve an unavailable book    |
| GET    | /api/reservations/my          | Authenticated | My active reservations         |
| GET    | /api/users                    | Admin/Librarian | List all users                |
| PUT    | /api/users/{id}/role          | Admin         | Update user role               |
| DELETE | /api/users/{id}               | Admin         | Delete user                    |
| GET    | /api/dashboard/stats          | Admin         | Library stats overview         |
| GET    | /api/dashboard/active-borrows | Admin         | All active borrow records      |

---

## 📦 Tech Stack

| Layer      | Technology                         |
|------------|------------------------------------|
| Frontend   | Angular 17 (standalone components) |
| Styling    | Vanilla CSS with glassmorphism     |
| Backend    | ASP.NET Core 8 Web API             |
| Database   | SQL Server / LocalDB               |
| ORM        | Entity Framework Core 8            |
| Auth       | JWT Bearer tokens + BCrypt         |
| Testing    | xUnit + EF Core InMemory           |
| API Docs   | Swagger / Swashbuckle              |
