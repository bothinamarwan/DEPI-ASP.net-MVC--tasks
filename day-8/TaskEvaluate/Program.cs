using Microsoft.EntityFrameworkCore;
using TaskEvaluate.Data.Contexts;
using TaskEvaluate.Repositories.Interfaces;
using TaskEvaluate.Repositories.Implementations;

namespace TaskEvaluate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // 2- Service CLR 
            #region services
            // Add services to the container.
            builder.Services.AddControllersWithViews(); 
            builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
            // NOTE: Connection strings are stored in appsettings.json. 
            // In production, use Environment Variables or Azure Key Vault/Secret Manager for better security.
            builder.Services.AddDbContext<Test06DbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Test01"));
            });
            #endregion

            var app = builder.Build();

            #region HTTP request pipeline
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // 1. Custom URL route
            app.MapControllerRoute(
                name: "AllTasks",
                pattern: "Tasks/All",
                defaults: new { controller = "TaskItem", action = "Index" });

            // 2. Custom route with optional parameters
            app.MapControllerRoute(
                name: "TaskManage",
                pattern: "Task/Manage/{id?}",
                defaults: new { controller = "TaskItem", action = "Index" });

            // 3. Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // 4. Fallback route for handling 404 or unknown paths
            app.MapFallbackToController("Error", "Home");

            app.Run(); 
            #endregion
        }
    }
}
