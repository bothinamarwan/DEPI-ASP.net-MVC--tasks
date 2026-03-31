using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using MVC_Day2.Models;
using System;
using MVC_Day2.Data;
using Microsoft.EntityFrameworkCore;
using MVC_Day2.ViewModels;
using Microsoft.Identity.Client;

namespace MVC_Day2.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly SchoolContext _context;

        public DepartmentController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ShowAll()
        {
           
            var departments = await _context.Departments
                .Include(d => d.Students)   
                .Include(d => d.Courses)
                .ToListAsync();

           
            return View(departments);       
        }

      
        public async Task<IActionResult> ShowDetails(int id)
        {
           
            var department = await _context.Departments
                .Include(d => d.Students)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                return NotFound($"No department found with Id = {id}");

            var vm = new DepartmentDetailsViewModel
            {
                DepartmentId = department.Id,
                DepartmentName = department.Name,
                ManagerName = department.MgrName,
                TotalStudentCount = department.Students.Count,

               
                DepartmentState = department.Students.Count > 50 ? "Main" : "Branch",

               
                StudentsOver25 = department.Students
                    .Where(s => s.Age > 25)
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    })
                    .ToList()
            };

            
            return View(vm);    
        }

     
        [HttpGet]
        public IActionResult Add()
        {
            var vm = new DepartmentAddViewModel();
            return View(vm);    
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DepartmentAddViewModel vm)
        {
            
            if (!ModelState.IsValid)
                return View(vm);    

            var department = new Department
            {
                Name = vm.Name,
                MgrName = vm.MgrName
            };

           
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowAll));

            
        }
        public IActionResult test()
        {
            return View();
        }
    }
}

