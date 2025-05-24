using CRMSystem.API.Models.Auth;
using CRMSystem.Domain.Entities;
using CRMSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static CRMSystem.Web.ViewModels.UserDto;

namespace CRMSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CRMDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<UserController> _logger;

        public UserController(CRMDbContext context, IPasswordHasher<User> passwordHasher, ILogger<UserController> logger)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.Users.Select(u => new
            {
                u.Id,
                u.Username,
                u.Role,
                u.CreatedAt
            }).ToList();

            return Ok(users);
        }

        // Yeni kullanıcı oluştur
        [HttpPost]
        public IActionResult Create(CreateUserDto dto)
        {
            if (_context.Users.Any(u => u.Username == dto.Username))
                return BadRequest("Bu kullanıcı adı zaten mevcut.");

            var user = new User
            {
                Username = dto.Username,
                Role = dto.Role,
                CreatedAt = "01.01.2000",
                UpdatedAt = "01.01.2000",

            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Kullanıcı başarıyla oluşturuldu.");
        }

        // Kullanıcı güncelle
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateUserDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");

            user.Role = dto.Role;
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            user.UpdatedAt = "01.01.2000";

            _context.SaveChanges();
            return Ok("Kullanıcı güncellendi.");
        }

        // Kullanıcı sil
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");

            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok("Kullanıcı silindi.");
        }

        private string? GetUsernameFromClaims()
        {
            return User.Claims.FirstOrDefault(c =>
        c.Type == JwtRegisteredClaimNames.Sub
    )?.Value;
        }
        
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CurrentPassword) || string.IsNullOrWhiteSpace(request.NewPassword))
                return BadRequest("Eksik bilgi.");

            // Kullanıcı adını token içindeki 'sub' claim'inden alıyoruz
            var username = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            var isValid = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash);
            if (!isValid)
                return BadRequest("Mevcut şifre hatalı.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.UpdatedAt = "01.01.2000";

            await _context.SaveChangesAsync();

            _logger.LogInformation($"[PasswordChanged] Kullanıcı: {username}");

            return Ok("Şifre başarıyla güncellendi.");
        }
    }
}
