using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Day2.Data;
using MVC_Day2.Models;
using MVC_Day2.ViewModels;
using System;

namespace MVC_Day2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;
        private const int PageSize = 5;   

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetAll(
            string? searchName = null,
            int? filterDepartmentId = null,
            int page = 1)
        {
          
            IQueryable<Student> query = _context.Students
                .Include(s => s.Department)
                .AsNoTracking();          

            
            if (!string.IsNullOrWhiteSpace(searchName))
                query = query.Where(s => s.Name.Contains(searchName));

            if (filterDepartmentId.HasValue)
                query = query.Where(s => s.DepartmentId == filterDepartmentId.Value);

            int totalRecords = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

           
            if (page < 1) page = 1;
            if (totalPages > 0 && page > totalPages) page = totalPages;

           
            var students = await query
                .OrderBy(s => s.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(s => new StudentRowViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age,
                    DepartmentName = s.Department.Name
                })
                .ToListAsync();

            var vm = new StudentGetAllViewModel
            {
                Students = students,
                SearchName = searchName,
                FilterDepartmentId = filterDepartmentId,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalRecords = totalRecords,
                PageSize = PageSize,
                DepartmentOptions = await BuildDepartmentOptionsAsync(includeAll: true)
            };

            return View(vm);
        }

        public async Task<IActionResult> GetById(int id)
        {
         
            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.StuCrsRes)
                    .ThenInclude(sc => sc.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return NotFound($"No student found with Id = {id}");

            var vm = new StudentFormViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                DepartmentId = student.DepartmentId,
                DepartmentOptions = new List<SelectListItem>
            {
                new SelectListItem(student.Department.Name, student.DepartmentId.ToString())
            }
            };

            ViewBag.Courses = student.StuCrsRes
                .Select(sc => new { CourseName = sc.Course.Name, sc.Grade })
                .ToList();

            return View(vm);
        }

      
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            
            var vm = new StudentFormViewModel
            {
                DepartmentOptions = await BuildDepartmentOptionsAsync()
            };

          
            return View("Form", vm);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StudentFormViewModel vm)
        {
          
            if (!ModelState.IsValid)
            {
                vm.DepartmentOptions = await BuildDepartmentOptionsAsync();
                return View("Form", vm);
            }

            var student = new Student
            {
                Name = vm.Name,
                Age = vm.Age,
                DepartmentId = vm.DepartmentId
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Student '{student.Name}' added successfully.";
            return RedirectToAction(nameof(GetAll));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return NotFound($"No student found with Id = {id}");

            var vm = new StudentFormViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                DepartmentId = student.DepartmentId,
                DepartmentOptions = await BuildDepartmentOptionsAsync()
            };

            return View("Form", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.DepartmentOptions = await BuildDepartmentOptionsAsync();
                return View("Form", vm);
            }

            var student = await _context.Students.FindAsync(vm.Id);
            if (student == null)
                return NotFound($"No student found with Id = {vm.Id}");

            student.Name = vm.Name;
            student.Age = vm.Age;
            student.DepartmentId = vm.DepartmentId;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Student '{student.Name}' updated successfully.";

            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.StuCrsRes)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
                return NotFound($"No student found with Id = {id}");

            var vm = new StudentDeleteViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                DepartmentName = student.Department.Name,
                EnrolledCourses = student.StuCrsRes.Count
            };

            return View(vm);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students
                .Include(s => s.StuCrsRes)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return NotFound($"No student found with Id = {id}");

            string name = student.Name;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Student '{name}' was permanently deleted.";

            return RedirectToAction(nameof(GetAll));
        }

        private async Task<List<SelectListItem>> BuildDepartmentOptionsAsync(bool includeAll = false)
        {
            var items = await _context.Departments
                .AsNoTracking()
                .OrderBy(d => d.Name)
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
                .ToListAsync();

            if (includeAll)
                items.Insert(0, new SelectListItem("All Departments", ""));

            return items;
        }
    }
}






