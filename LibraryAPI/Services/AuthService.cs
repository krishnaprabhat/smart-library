using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryAPI.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByStudentIdAsync(dto.StudentId);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            return new AuthResponseDto
            {
                Token = GenerateJwt(user),
                Name = user.Name,
                Role = user.Role.ToString(),
                UserId = user.Id
            };
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
        {
            var existing = await _userRepo.GetByStudentIdAsync(dto.StudentId);
            if (existing != null) return null;

            var user = new User
            {
                Name = dto.Name,
                StudentId = dto.StudentId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = UserRole.Student
            };

            await _userRepo.AddAsync(user);

            return new AuthResponseDto
            {
                Token = GenerateJwt(user),
                Name = user.Name,
                Role = user.Role.ToString(),
                UserId = user.Id
            };
        }

        private string GenerateJwt(User user)
        {
            var settings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings["SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.StudentId),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: settings["Issuer"],
                audience: settings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(settings["ExpiryMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
