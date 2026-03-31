namespace MVC_Day2.Models
{
    public class StuCrsRes
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Grade { get; set; } 

        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
