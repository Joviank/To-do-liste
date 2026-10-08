namespace TaskService.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase
{
    private readonly Core.TaskService taskManager;

    public TaskController(Core.TaskService taskService)
    {
        taskManager = taskService;
    }

    [HttpGet]
    public IEnumerable<TaskItem> GetTasks()
    {
        return taskManager.GetTasks();
    }

    [HttpPost]
    public TaskItem AddTask(string title)
    {
        return taskManager.AddTask(title);
    }

    [HttpPatch("{id}/complete")]
    public IActionResult CompleteTask(Guid id)
    {
        taskManager.CompleteTask(id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(Guid id)
    {
        taskManager.DeleteTask(id);
        return NoContent();
    }
}