using Microsoft.EntityFrameworkCore;
using MVC_Day2.Models;

namespace MVC_Day2.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StuCrsRes> StuCrsRes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///// Composite PK for junction table
            modelBuilder.Entity<StuCrsRes>()
                .HasKey(s => new { s.StudentId, s.CourseId });

            ///////////// StuCrsRes -> Student
            modelBuilder.Entity<StuCrsRes>()
                .HasOne(s => s.Student)
                .WithMany(st => st.StuCrsRes)
                .HasForeignKey(s => s.StudentId);

            /////////////////////// StuCrsRes -> Course
            modelBuilder.Entity<StuCrsRes>()
                .HasOne(s => s.Course)
                .WithMany(c => c.StuCrsRes)
                .HasForeignKey(s => s.CourseId);

            //////////////////// Teacher -> Course (many-to-one)
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Course)
                .WithMany(c => c.Teachers)
                .HasForeignKey(t => t.CourseId);

            //////////////////////// Teacher -> Department
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.DepartmentId);

            ////////////////////////////////// Student -> Department
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId);

            //////// Course -> Department
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId);

            /////////// Seed data
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Computer Science", MgrName = "Dr. Smith" },
                new Department { Id = 2, Name = "Mathematics", MgrName = "Dr. Jones" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Algorithms", Degree = 80, MinDegree = 50, DepartmentId = 1 },
                new Course { Id = 2, Name = "Calculus", Degree = 65, MinDegree = 50, DepartmentId = 2 },
                new Course { Id = 3, Name = "Database Systems", Degree = 90, MinDegree = 50, DepartmentId = 1 }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Alice Johnson", Age = 20, DepartmentId = 1 },
                new Student { Id = 2, Name = "Bob Williams", Age = 22, DepartmentId = 2 },
                new Student { Id = 3, Name = "Carol Davis", Age = 21, DepartmentId = 1 },
                new Student { Id = 4, Name = "David Brown", Age = 23, DepartmentId = 2 }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, Name = "Prof. Anderson", Salary = 75000, Address = "123 Elm St", CourseId = 1, DepartmentId = 1 },
                new Teacher { Id = 2, Name = "Prof. Taylor", Salary = 68000, Address = "456 Oak Ave", CourseId = 2, DepartmentId = 2 }
            );

            modelBuilder.Entity<StuCrsRes>().HasData(
                new StuCrsRes { StudentId = 1, CourseId = 1, Grade = 80 },
                new StuCrsRes { StudentId = 1, CourseId = 3, Grade = 75 },
                new StuCrsRes { StudentId = 2, CourseId = 2, Grade = 95 },
                new StuCrsRes { StudentId = 3, CourseId = 1, Grade = 96 },
                new StuCrsRes { StudentId = 3, CourseId = 3, Grade = 55 },
                new StuCrsRes { StudentId = 4, CourseId = 2, Grade = 66 }
            );
        }
    }
}