using Microsoft.EntityFrameworkCore;
using MVC_Day2.Models;

namespace MVC_Day2.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StuCrsRes> StuCrsRes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ===== Many-to-Many: Student-Course =====
            modelBuilder.Entity<StuCrsRes>()
                .HasKey(x => new { x.StudentId, x.CourseId });

            modelBuilder.Entity<StuCrsRes>()
                .HasOne(x => x.Student)
                .WithMany(s => s.StuCrsRes)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StuCrsRes>()
                .HasOne(x => x.Course)
                .WithMany(c => c.StuCrsRes)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== Teacher =====
            modelBuilder.Entity<Teacher>()
                .Property(t => t.Salary)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Course)
                .WithMany(c => c.Teachers)
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== Seed Data: Departments =====
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Computer Science", MgrName = "Dr. Hassan" },
                new Department { Id = 2, Name = "Mathematics", MgrName = "Dr. Fatma" }
            );

            // ===== Seed Data: Courses =====
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Programming 101", DepartmentId = 1 },
                new Course { Id = 2, Name = "Algorithms", DepartmentId = 1 },
                new Course { Id = 3, Name = "Calculus", DepartmentId = 2 }
            );

            // ===== Seed Data: Students =====
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Ali", Age = 20, DepartmentId = 1 },
                new Student { Id = 2, Name = "Sara", Age = 21, DepartmentId = 1 },
                new Student { Id = 3, Name = "Omar", Age = 22, DepartmentId = 2 }
            );

            // ===== Seed Data: Student-Course Results =====
            modelBuilder.Entity<StuCrsRes>().HasData(
                new StuCrsRes { StudentId = 1, CourseId = 1, Grade = 90 },
                new StuCrsRes { StudentId = 1, CourseId = 2, Grade = 85 },
                new StuCrsRes { StudentId = 2, CourseId = 1, Grade = 88 },
                new StuCrsRes { StudentId = 3, CourseId = 3, Grade = 92 }
            );
        }
    }
}