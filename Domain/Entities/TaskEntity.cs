
namespace ThunderAPI.Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? DueDate { get; private set; }
        public bool IsCompleted { get; private set; }
        public TaskEntity() { }
        public TaskEntity(string title, string description, DateTime? dueDate)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Title cannot be empty or null.");

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            DueDate = dueDate;
            IsCompleted = false;
        }

        public void CompleteTask()
        {
            if (IsCompleted)
                throw new InvalidOperationException("Task is already completed.");

            IsCompleted = true;
        }

        public void UpdateTaskDetails(string title, string description, DateTime? dueDate)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Title cannot be empty or null.");

            Title = title;
            Description = description;
            DueDate = dueDate;
        }
    }
}
