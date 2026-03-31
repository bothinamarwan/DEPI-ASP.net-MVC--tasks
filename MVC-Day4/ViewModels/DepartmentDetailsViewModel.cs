using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_Day2.ViewModels
{
    public class DepartmentDetailsViewModel
    {
        //////////////Basic department info 
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string ManagerName { get; set; } = string.Empty;

        //////////////////////// Computed / derived property
       
        public string DepartmentState { get; set; } = string.Empty;

        public int TotalStudentCount { get; set; }

        ///////////Dropdown list 
        public List<SelectListItem> StudentsOver25 { get; set; } = new();

        public int? SelectedStudentId { get; set; }
    }
}

