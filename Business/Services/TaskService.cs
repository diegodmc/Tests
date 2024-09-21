
using ThunderAPI.Domain.Contracts.Requests;
using ThunderAPI.Domain.Entities;
using ThunderAPI.Domain.Interfaces;

namespace ThunderAPI.Business.Services
{
	public class TaskService : ITaskService
	{
		private readonly ITaskRepository _taskRepository;

		public TaskService(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public async Task<TaskEntity> GetTaskByIdAsync(Guid id)
		{
			try
			{
				return await _taskRepository.GetByIdAsync(id);
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Error GetTaskByIdAsync : {id} ", ex);
			}
		}

		public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync()
		{
			try
			{
				return (IEnumerable<TaskEntity>)await _taskRepository.GetAllAsync();
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Error GetAllTasksAsync ", ex);
			}
		}

		public async Task<TaskEntity> CreateTaskAsync(CreateTaskRequest request)
		{
			try
			{
				var task = new TaskEntity(request.Title, request.Description, request.DueDate);
				await _taskRepository.AddAsync(task);
				return task;
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Error CreateTaskAsync: {request.Title} ", ex);
			}
		}

		public async Task<TaskEntity> UpdateTaskAsync(Guid id, UpdateTaskRequest request)
		{
			try
			{
				var task = await _taskRepository.GetByIdAsync(id);
				if (task == null)
					return null;

				task.UpdateTaskDetails(request.Title, request.Description, request.DueDate);
				task.CompleteTask();

				await _taskRepository.UpdateAsync(task);
				return task;
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Error UpdateTaskAsync: {id} ", ex);
			}
		}

		public async Task<bool> DeleteTaskAsync(Guid id)
		{
			try
			{
				var task = await _taskRepository.GetByIdAsync(id);
				if (task == null)
					return false;

				await _taskRepository.DeleteAsync(id);
				return true;
			}
			catch (Exception ex)
			{
				throw new ApplicationException($"Error DeleteTaskAsync: {id} ", ex);
			}
		}
	}
}
