using Microsoft.AspNetCore.Mvc;
using ThunderAPI.Domain.Contracts.Requests;
using ThunderAPI.Api.HttpResponseCommon;
using ThunderAPI.Domain.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskRequest request)
    {
        var task = await _taskService.CreateTaskAsync(request);
        return Ok(ApiResponse.FromSuccess(task));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskRequest request)
    {
        var task = await _taskService.UpdateTaskAsync(id, request);
        return Ok(ApiResponse.FromSuccess(task));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
            return NoContent();

        return Ok(ApiResponse.FromSuccess(task));
    }
    [HttpGet]
    public async Task<IActionResult> GetTaskAll()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        if (tasks == null)
            return NoContent();

        return Ok(ApiResponse.FromSuccess(tasks));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var result = await _taskService.DeleteTaskAsync(id);
        if (!result)
            return NoContent();

        return Ok(ApiResponse.FromSuccess("Task deleted successfully"));
    }

}
