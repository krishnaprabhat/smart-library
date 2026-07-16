using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public class UserAdminService
    {
        private readonly IUserRepository _userRepo;
        private readonly IBorrowRepository _borrowRepo;

        public UserAdminService(IUserRepository userRepo, IBorrowRepository borrowRepo)
        {
            _userRepo = userRepo;
            _borrowRepo = borrowRepo;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            var borrows = await _borrowRepo.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                StudentId = u.StudentId,
                Role = u.Role.ToString(),
                Phone = u.Phone,
                Department = u.Department,
                CreatedAt = u.CreatedAt,
                BorrowCount = borrows.Count(b => b.UserId == u.Id && !b.IsReturned)
            }).ToList();
        }

        public async Task<bool> UpdateRoleAsync(int userId, string role)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;
            if (!Enum.TryParse<UserRole>(role, out var parsedRole)) return false;
            user.Role = parsedRole;
            await _userRepo.UpdateAsync(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;
            await _userRepo.DeleteAsync(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProfileAsync(int userId, UpdateProfileDto dto)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;
            user.Name = dto.Name;
            user.Phone = dto.Phone;
            user.Department = dto.Department;
            await _userRepo.UpdateAsync(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }
    }
}
