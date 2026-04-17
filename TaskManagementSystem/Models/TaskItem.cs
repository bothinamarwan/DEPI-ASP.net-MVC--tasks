using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
       [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? DueDate { get; set; }
    }
}
