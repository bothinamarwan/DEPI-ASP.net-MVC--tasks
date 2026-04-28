using TaskEvaluate.Data.Contexts;
using TaskEvaluate.Models;
using TaskEvaluate.Repositories.Interfaces;

namespace TaskEvaluate.Repositories.Implementations
{
    public class TaskItemRepository : GenericRepository<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(Test06DbContext context) : base(context)
        {
        }

        // Add specific implementations for TaskItem methods here if needed
    }
}
