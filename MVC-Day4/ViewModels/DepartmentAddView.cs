using System.ComponentModel.DataAnnotations;

namespace MVC_Day2.ViewModels
{
    public class DepartmentAddViewModel
    {
        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Department Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Manager name is required.")]
        [StringLength(100)]
        [Display(Name = "Manager Name")]
        public string MgrName { get; set; } = string.Empty;
    }
}
