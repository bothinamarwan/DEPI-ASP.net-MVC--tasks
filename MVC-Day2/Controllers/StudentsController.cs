using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Day2.Data;

namespace MVC_Day2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: /Student/ShowAll
        public async Task<IActionResult> ShowAll()
        {
            var students = await _context.Students
                .Include(s => s.Department)
                .ToListAsync();
            return View(students);
        }

        // GET: /Student/ShowDetails?id=3
        public async Task<IActionResult> ShowDetails(int id)
        {
            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.StuCrsRes)
                    .ThenInclude(scr => scr.Course)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return NotFound();

            return View(student);
        }
    }
}
