

using ThunderAPI.Domain.Contracts.Requests;
using ThunderAPI.Domain.Entities;

namespace ThunderAPI.Domain.Interfaces
{
    public interface ITaskService
    {
        Task<TaskEntity> GetTaskByIdAsync(Guid id);
        Task<IEnumerable<TaskEntity>> GetAllTasksAsync();
        Task<TaskEntity> CreateTaskAsync(CreateTaskRequest request);
        Task<TaskEntity> UpdateTaskAsync(Guid id, UpdateTaskRequest request);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}
