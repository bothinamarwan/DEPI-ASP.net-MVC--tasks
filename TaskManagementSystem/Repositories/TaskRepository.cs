using TaskManagementSystem.Data.DbContexts;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Repositories
{
    public class TaskRepository : ITaskRepository
    {
		private readonly AppDbContext _context = new AppDbContext();

		public List<TaskItem> GetAll()
		{
			return _context.Tasks.ToList();
		}

		public TaskItem GetById(int id)
		{
			return _context.Tasks.FirstOrDefault(t => t.Id == id);
		}

		public void Add(TaskItem task)
		{
			_context.Tasks.Add(task);
			_context.SaveChanges();
		}

		public void Update(TaskItem task)
		{
			_context.Tasks.Update(task);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var task = GetById(id);
			if (task != null)
			{
				_context.Tasks.Remove(task);
				_context.SaveChanges();
			}
		}

		// BONUS
		public List<TaskItem> GetByStatus(bool status)
		{
			return _context.Tasks
				.Where(t => t.IsCompleted == status)
				.ToList();
		}

		// BONUS
		public List<TaskItem> SortByDueDate()
		{
			return _context.Tasks
				.OrderBy(t => t.DueDate)
				.ToList();
		}
	}
}
