using Microsoft.EntityFrameworkCore;
using TaskEvaluate.Data.Configurations;
using TaskEvaluate.Models;

namespace TaskEvaluate.Data.Contexts
{
    public class Test06DbContext : DbContext
    {
        //// Configure connection 
        //protected override void onconfiguring(dbcontextoptionsbuilder optionsbuilder)
        //{
        //    optionsbuilder.usesqlserver("data source=.;initial catalog=test06;integrated security=true;encrypt=false;trust server certificate=true");
        //}

        // DI 
        // 1- ASk not create
        public Test06DbContext(DbContextOptions options) : base(options)
        {
            
        }

        // Read
        public DbSet<TaskItem> TaskItems { get; set; }

        // Execute mapping 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskItemConfiguration());
        }
    }
}
