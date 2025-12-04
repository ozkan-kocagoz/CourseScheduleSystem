using Microsoft.AspNetCore.Mvc;
using CourseScheduleApi.Data;
using CourseScheduleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseScheduleApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }
    }
}