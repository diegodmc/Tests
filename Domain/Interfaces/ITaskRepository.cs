
using ThunderAPI.Domain.Entities;

namespace ThunderAPI.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task AddAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(Guid id);
    }
}
