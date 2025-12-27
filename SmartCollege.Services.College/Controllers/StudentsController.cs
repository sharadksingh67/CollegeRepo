using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCollege.Services.College.Data;
using SmartCollege.Services.College.Models.Dto;

namespace SmartCollege.Services.College.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class StudentsController : ControllerBase
    {
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok("Only Admin can access this endpoint");
        //}

        private readonly CollegeDbContext _context;

        public StudentsController(CollegeDbContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var students = await _context.Students
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Course = s.Course,
                    Age = s.Age
                })
                .ToListAsync();

            return Ok(students);
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _context.Students
                .Where(s => s.Id == id)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Course = s.Course,
                    Age = s.Age
                })
                .FirstOrDefaultAsync();

            if (student == null)
                return NotFound();

            return Ok(student);
        }
    }
}
