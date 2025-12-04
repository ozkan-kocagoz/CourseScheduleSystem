using Microsoft.AspNetCore.Mvc;
using CourseScheduleApi.Data;
using CourseScheduleApi.Models;
using Microsoft.EntityFrameworkCore;
using MimeKit;         
using MailKit.Net.Smtp;

namespace CourseScheduleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Kullanıcı giriş işlemini gerçekleştirir.
        /// E-posta ve şifre doğrulaması yapar, başarılı ise kullanıcı bilgilerini döner.
        /// </summary>
        /// <param name="request">Giriş bilgilerini içeren model</param>
        /// <returns>Kullanıcı bilgileri veya hata mesajı</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.PasswordHash == request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Geçersiz e-posta veya şifre!" });
            }
            return Ok(new 
            { 
                message = "Giriş Başarılı", 
                userId = user.Id,
                fullName = user.FullName,
                role = user.Role,
                departmentId = user.DepartmentId
            });
        }

        /// <summary>
        /// Kullanıcı girişi yapar ve oturum bilgilerini döner.
        /// </summary>
        /// <param name="request">Giriş bilgileri (Email, Şifre)</param>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest(new { message = "Bu e-posta adresi zaten kayıtlı!" });
            }

            string randomPassword = new Random().Next(100000, 999999).ToString();

            var newUser = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Role = request.Role,
                PasswordHash = randomPassword,
                DepartmentId = request.DepartmentId
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Ders Yönetim Sistemi", "no-reply@university.edu.tr"));
                
                message.To.Add(new MailboxAddress(request.FullName, request.Email));
                
                message.Subject = "Sisteme Kaydınız Yapıldı - Giriş Bilgileri";
                message.Body = new TextPart("plain")
                {
                    Text = $@"Sayın {request.FullName},

Ders Programı Yönetim Sistemine kaydınız başarıyla oluşturulmuştur.
Aşağıdaki bilgilerle sisteme giriş yapabilirsiniz:

E-Posta: {request.Email}
Geçici Şifre: {randomPassword}

Güvenliğiniz için giriş yaptıktan sonra şifrenizi değiştirmenizi öneririz.

İyi çalışmalar dileriz."
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.ethereal.email", 587, false);
                    
                    client.Authenticate("monique.crona@ethereal.email ", "aaaDKxjejvuMrqhBQ6");
                    
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SMTP Hatası]: {ex.Message}");
                return Ok(new 
                { 
                    message = "Kullanıcı kaydedildi ancak mail gönderilemedi. (Şifreyi manuel veriniz)", 
                    password = randomPassword
                });
            }

            return Ok(new { message = "Kullanıcı başarıyla kaydedildi ve mail gönderildi!" });
        }
    }


    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int? DepartmentId { get; set; }
    }
}