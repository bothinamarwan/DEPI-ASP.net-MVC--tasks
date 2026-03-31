using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_Day2.ViewModels
{
  

    public class StudentGetAllViewModel
    {
        ///////////Result rows
        public List<StudentRowViewModel> Students { get; set; } = new();

        //Filter / Search inputs
        public string? SearchName { get; set; }
        public int? FilterDepartmentId { get; set; }

        //Dropdown for department filter
        public List<SelectListItem> DepartmentOptions { get; set; } = new();

        /////////// Pagination metadata
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;
        public int TotalRecords { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
    public class StudentRowViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
    }
}

