using Dapper;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using ThunderAPI.Business.Services;
using ThunderAPI.Domain.Contracts.Requests;
using ThunderAPI.Domain.Entities;

namespace Integration;
public class TaskServiceIntegrationTests
{
	private readonly TaskService _taskService;
	private readonly string _connectionString;

	public TaskServiceIntegrationTests()
	{
		_connectionString = "Server=host.docker.internal;Database=TASKDB;Uid=root;Pwd=1234;Port=3306;"; 
		var taskRepository = new TaskRepository(_connectionString);
		_taskService = new TaskService(taskRepository);

		InitializeDatabase();
	}

	private void InitializeDatabase()
	{
		using (var db = new MySqlConnection(_connectionString))
		{
			db.ExecuteAsync("CREATE DATABASE IF NOT EXISTS TASKDB;");
			db.Execute("DROP TABLE IF EXISTS TASKDB.Tasks;"); 
			db.Execute(@"
                    CREATE TABLE TASKDB.Tasks (
                        Id CHAR(36) PRIMARY KEY,
                        Title VARCHAR(255) NOT NULL,
                        Description TEXT,
                        DueDate DATETIME NOT NULL
                    );");
		}
	}

	[Fact]
	public async Task CreateTaskAsync_SavesTaskToDatabase()
	{
		// Arrange
		var request = new CreateTaskRequest
		{
			Title = "Test Task",
			Description = "Test Description",
			DueDate = DateTime.UtcNow.AddDays(1)
		};

		// Act
		var result = await _taskService.CreateTaskAsync(request);

		// Assert
		using (var db = new MySqlConnection(_connectionString))
		{
			var savedTask = await db.QuerySingleOrDefaultAsync<TaskEntity>("SELECT * FROM TASKDB.Tasks WHERE Id = @Id", new { Id = result.Id });
			Assert.NotNull(savedTask);
			Assert.Equal(result.Title, savedTask.Title);
		}
	}

	[Fact]
	public async Task GetTaskByIdAsync_ValidId_ReturnsTask()
	{
		// Arrange
		var task = new TaskEntity("Existing Task", "Description", DateTime.UtcNow);
		using (var db = new MySqlConnection(_connectionString))
		{
			await db.ExecuteAsync("INSERT INTO TASKDB.Tasks (Id, Title, Description, DueDate) VALUES (@Id, @Title, @Description, @DueDate)", task);
		}

		// Act
		var result = await _taskService.GetTaskByIdAsync(task.Id);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(task.Title, result.Title);
	}

	[Fact]
	public async Task UpdateTaskAsync_ValidId_UpdatesTask()
	{
		// Arrange
		var task = new TaskEntity("Old Title", "Old Description", DateTime.UtcNow);
		using (var db = new MySqlConnection(_connectionString))
		{
			await db.ExecuteAsync("INSERT INTO TASKDB.Tasks (Id, Title, Description, DueDate) VALUES (@Id, @Title, @Description, @DueDate)", task);
		}

		var updateRequest = new UpdateTaskRequest
		{
			Title = "Updated Title",
			Description = "Updated Description",
			DueDate = DateTime.UtcNow.AddDays(2)
		};

		// Act
		var result = await _taskService.UpdateTaskAsync(task.Id, updateRequest);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(updateRequest.Title, result.Title);
	}

	[Fact]
	public async Task DeleteTaskAsync_ValidId_DeletesTask()
	{
		// Arrange
		var task = new TaskEntity("Task to Delete", "Description", DateTime.UtcNow);
		
		using (var db = new MySqlConnection(_connectionString))
		{
			await db.ExecuteAsync("INSERT INTO TASKDB.Tasks (Id, Title, Description, DueDate) VALUES (@Id, @Title, @Description, @DueDate)", task);
		}

		// Act
		var result = await _taskService.DeleteTaskAsync(task.Id);

		// Assert
		Assert.True(result);
		using (var db = new MySqlConnection(_connectionString))
		{
			var deletedTask = await db.QuerySingleOrDefaultAsync<TaskEntity>("SELECT * FROM TASKDB.Tasks WHERE Id = @Id", new { Id = task.Id });
			Assert.Null(deletedTask);
		}
	}

	[Fact]
	public async Task GetAllTasksAsync_ReturnsAllTasks()
	{
		// Arrange
		var tasks = new List<TaskEntity>
			{
				new TaskEntity( "Task 1", "Description 1", DateTime.UtcNow),
				new TaskEntity( "Task 2", "Description 2", DateTime.UtcNow)
			};

		using (var db = new MySqlConnection(_connectionString))
		{
			await db.ExecuteAsync("INSERT INTO TASKDB.Tasks (Id, Title, Description, DueDate) VALUES (@Id, @Title, @Description, @DueDate)", tasks);
		}

		// Act
		var result = await _taskService.GetAllTasksAsync();

		// Assert
		Assert.Equal(tasks.Count, result.Count());
	}
}