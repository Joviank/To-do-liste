using Microsoft.AspNetCore.Mvc;
using TaskService.Api.DTO;
using TaskService.Api.Services;
namespace TaskService.Api.Controllers;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase
{
    private readonly Core.TaskService taskManager;
    private readonly ITokenReader tokenReader;

    public TaskController(Core.TaskService taskService, ITokenReader tokenReader)
    {
        taskManager = taskService;
        this.tokenReader = tokenReader;
    }

    private Guid? GetUserId()
    {
        return tokenReader.GetUserId(Request.Headers.Authorization.ToString());
    }

    [HttpGet]
    public IActionResult GetTasks()
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }
        return Ok(taskManager.GetTasks(userId.Value));
    }

    [HttpPost]
    public IActionResult AddTask([FromBody]CreateTaskRequest request)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        return Ok(taskManager.AddTask(request.Title, userId.Value));
    }

    [HttpPatch("{id}/complete")]
    public IActionResult CompleteTask(Guid id)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        taskManager.CompleteTask(id, userId.Value);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(Guid id)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        taskManager.DeleteTask(id, userId.Value);
        return NoContent();
    }
}