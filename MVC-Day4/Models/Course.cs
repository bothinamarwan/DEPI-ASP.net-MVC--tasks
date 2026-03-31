namespace MVC_Day2.Models
{
        public class Course
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public double Degree { get; set; }      
            public double MinDegree { get; set; }   
            public int DepartmentId { get; set; }

            public Department Department { get; set; } = null!;
            public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
            public ICollection<StuCrsRes> StuCrsRes { get; set; } = new List<StuCrsRes>();
        }
}

