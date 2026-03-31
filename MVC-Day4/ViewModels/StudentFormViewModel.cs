using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVC_Day2.ViewModels
{
    public class StudentFormViewModel
    {
        public int Id { get; set; }   

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2–100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Age is required.")]
        [Range(16, 100, ErrorMessage = "Age must be between 16 and 100.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please select a department.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        ////// dropdown list populated by controller, never posted back 
        public List<SelectListItem> DepartmentOptions { get; set; } = new();

        ///// Helper so the view can show the correct heading
        public bool IsEdit => Id > 0;
    }
}

