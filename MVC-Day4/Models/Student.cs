namespace MVC_Day2.Models
{
    public class Student
    {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int Age { get; set; }
            public int DepartmentId { get; set; }

            public Department Department { get; set; } = null!;
            public ICollection<StuCrsRes> StuCrsRes { get; set; } = new List<StuCrsRes>();
        
    }
}
