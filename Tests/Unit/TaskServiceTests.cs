namespace Unit;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using ThunderAPI.Domain.Contracts.Requests;
using ThunderAPI.Business.Services;
using ThunderAPI.Domain.Entities;
using ThunderAPI.Domain.Interfaces;
using Xunit;
public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _mockTaskRepository = new Mock<ITaskRepository>();
        _taskService = new TaskService(_mockTaskRepository.Object);
    }

    [Fact]
    public async Task GetTaskByIdAsync_ValidId_ReturnsTask()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var expectedTask = new TaskEntity("Test Title", "Test Description", DateTime.UtcNow);
        _mockTaskRepository.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync(expectedTask);

        // Act
        var result = await _taskService.GetTaskByIdAsync(taskId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedTask.Title, result.Title);
    }

    [Fact]
    public async Task GetAllTasksAsync_ReturnsAllTasks()
    {
        // Arrange
        var expectedTasks = new List<TaskEntity>
            {
                new TaskEntity("Task 1", "Description 1", DateTime.UtcNow),
                new TaskEntity("Task 2", "Description 2", DateTime.UtcNow)
            };
        _mockTaskRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedTasks);

        // Act
        var result = await _taskService.GetAllTasksAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedTasks.Count, result.Count());
    }

    [Fact]
    public async Task CreateTaskAsync_ValidRequest_CreatesTask()
    {
        // Arrange
        var request = new CreateTaskRequest
        {
            Title = "New Task",
            Description = "New Task Description",
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = await _taskService.CreateTaskAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.Title, result.Title);
        _mockTaskRepository.Verify(repo => repo.AddAsync(It.IsAny<TaskEntity>()), Times.Once);
    }

    [Fact]
    public async Task UpdateTaskAsync_ValidId_UpdatesTask()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var existingTask = new TaskEntity("Old Title", "Old Description", DateTime.UtcNow);
        var updateRequest = new UpdateTaskRequest
        {
            Title = "Updated Title",
            Description = "Updated Description",
            DueDate = DateTime.UtcNow.AddDays(2)
        };

        _mockTaskRepository.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync(existingTask);

        // Act
        var result = await _taskService.UpdateTaskAsync(taskId, updateRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updateRequest.Title, result.Title);
        _mockTaskRepository.Verify(repo => repo.UpdateAsync(It.IsAny<TaskEntity>()), Times.Once);
    }

    [Fact]
    public async Task DeleteTaskAsync_ValidId_DeletesTask()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var existingTask = new TaskEntity("Test Title", "Test Description", DateTime.UtcNow);
        _mockTaskRepository.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync(existingTask);

        // Act
        var result = await _taskService.DeleteTaskAsync(taskId);

        // Assert
        Assert.True(result);
        _mockTaskRepository.Verify(repo => repo.DeleteAsync(taskId), Times.Once);
    }

    [Fact]
    public async Task DeleteTaskAsync_InvalidId_ReturnsFalse()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        _mockTaskRepository.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync((TaskEntity)null);

        // Act
        var result = await _taskService.DeleteTaskAsync(taskId);

        // Assert
        Assert.False(result);
    }
}