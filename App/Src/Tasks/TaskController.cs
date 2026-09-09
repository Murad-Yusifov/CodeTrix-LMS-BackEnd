using Microsoft.AspNetCore.Mvc;
using BackEndCodeTrix.Src.Tasks.TaskDTO;
using BackEndCodeTrix.Src.Group.GroupDTO;

namespace BackEndCodeTrix.Src.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(
        ITaskService taskService
    )
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await _taskService
            .GetAllAsync();

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(
        int id
    )
    {
        var task = await _taskService
            .GetByIdAsync(id);

        if (task is null)
        {
            return NotFound("Task not found.");
        }

        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(
        CreateTaskDto dto
    )
    {
        try
        {
            var task = await _taskService
                .CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetTaskById),
                new
                {
                    id = task.TaskId
                },
                task
            );
        }
        catch (Exception exception)
        {
            return BadRequest(
                exception.Message
            );
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(
        int id,
        UpdateTaskDto dto
    )
    {
        var task = await _taskService
            .UpdateAsync(id, dto);

        if (task is null)
        {
            return NotFound("Task not found.");
        }

        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(
        int id
    )
    {
        var deleted = await _taskService
            .DeleteAsync(id);

        if (!deleted)
        {
            return NotFound("Task not found.");
        }

        return NoContent();
    }

     [HttpGet("{taskId}/students")]
    public async Task<IActionResult> GetTaskStudent (int taskId)
    {
        var group = await _taskService.GetStudentsByTaskIdAsync(taskId);
        if (group is null)
            return NotFound("Student is not found");

        return Ok(group);

    }

    [HttpGet("{taskId}/group")]
    public async Task<IActionResult> GetTaskGroup(int taskId)
    {
        var group = await _taskService.GetGroupByTaskIdAsync(taskId);
        if (group is null)
            return NotFound("Group is not found");

        return Ok(group);

    }
}