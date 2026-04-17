using TaskManagementSystem.Models;

namespace TaskManagementSystem.Repositories
{
    public interface ITaskRepository
    {
		//List<TaskItem> GetAll();
		//
		//TaskItem GetById(int id);
		//
		//void Add(TaskItem task);
		//
		//void Update(TaskItem task);
		//
		//void Delete(int id);
		List<TaskItem> GetAll();
		TaskItem GetById(int id);
		void Add(TaskItem task);
		void Update(TaskItem task);
		void Delete(int id);

		List<TaskItem> GetByStatus(bool status);   // BONUS
		List<TaskItem> SortByDueDate();
	}
}
