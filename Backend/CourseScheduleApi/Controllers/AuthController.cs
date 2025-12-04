using Microsoft.AspNetCore.Mvc;
using CourseScheduleApi.Data;
using CourseScheduleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseScheduleApi.Controllers
{
    [Route("api/[controller]")] // Adres: /api/auth
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // Login iþlemi: POST /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // 1. Veritabanýnda bu email var mý ve þifresi tutuyor mu?
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.PasswordHash == request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Gecersiz email veya sifre!" });
            }

            // 2. Giriþ baþarýlý, kullanýcý bilgilerini dön (Role önemli!)
            return Ok(new
            {
                message = "Giris Basarili",
                userId = user.Id,
                fullName = user.FullName,
                role = user.Role, // Frontend bu role göre yönlendirme yapacak
                departmentId = user.DepartmentId
            });
        }
    }

    // Login isteði için kullanýlacak küçük model
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}