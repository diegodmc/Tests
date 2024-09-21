
using MySql.Data.MySqlClient;
using System.Data;
using ThunderAPI.Domain.Entities;
using ThunderAPI.Domain.Interfaces;
using Dapper;

public class TaskRepository : ITaskRepository
{
	private readonly string _connectionString;

	public TaskRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<TaskEntity> GetByIdAsync(Guid id)
	{
		try
		{
			using (IDbConnection db = new MySqlConnection(_connectionString))
			{
				return await db.QuerySingleOrDefaultAsync<TaskEntity>(TaskSql.GetTaskById, new { Id = id });
			}
		}
		catch (MySqlException ex)
		{
			throw new ApplicationException($"Error GetByIdAsync: {id} ", ex);
		}
	}

	public async Task<IEnumerable<TaskEntity>> GetAllAsync()
	{
		try
		{
			using (IDbConnection db = new MySqlConnection(_connectionString))
			{
				return await db.QueryAsync<TaskEntity>(TaskSql.GetAllTasks);
			}
		}
		catch (MySqlException ex)
		{
			throw new ApplicationException($"Error GetAllAsync ", ex);
		}
	}

	public async Task AddAsync(TaskEntity task)
	{
		try
		{
			using (IDbConnection db = new MySqlConnection(_connectionString))
			{
				await db.ExecuteAsync(TaskSql.AddTask, task);
			}
		}
		catch (MySqlException ex)
		{
			throw new ApplicationException($"Error AddAsync: {task.Title} ", ex);
		}
	}

	public async Task UpdateAsync(TaskEntity task)
	{
		try
		{
			using (IDbConnection db = new MySqlConnection(_connectionString))
			{
				await db.ExecuteAsync(TaskSql.UpdateTask, task);
			}
		}
		catch (MySqlException ex)
		{
			throw new ApplicationException($"Error UpdateAsync: {task.Title} ", ex);
		}
	}

	public async Task DeleteAsync(Guid id)
	{
		try
		{
			using (IDbConnection db = new MySqlConnection(_connectionString))
			{
				await db.ExecuteAsync(TaskSql.DeleteTask, new { Id = id });
			}
		}
		catch (MySqlException ex)
		{
			throw new ApplicationException($"Error DeleteAsync: {id} ", ex);
		}
	}
}