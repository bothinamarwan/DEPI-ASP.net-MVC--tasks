using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Data.DbContexts
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=TaskDB;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<TaskItem> Tasks { get; set; }
		public AppDbContext(DbContextOptions<AppDbContext> options)
	: base(options)
		{
		}
	}
}
