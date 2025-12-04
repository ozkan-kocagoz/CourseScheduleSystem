using Microsoft.AspNetCore.Mvc;
using CourseScheduleApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseScheduleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new { u.Id, u.FullName, u.Role })
                .ToListAsync();

            return Ok(users);
        }
    }
}   