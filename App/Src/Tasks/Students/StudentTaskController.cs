using Microsoft.AspNetCore.Mvc;
using BackEndCodeTrix.Src.Tasks.StudentTaskDTO;
using BackEndCodeTrix.Src.Tasks.Students;

// namespace BackEndCodeTrix.Src.Tasks;

[ApiController]
[Route("api/[controller]")]
public class StudentTaskController : ControllerBase
{
    private readonly IStudentTaskService _studentTaskService;

    public StudentTaskController(
        IStudentTaskService studentTaskService
    )
    {
        _studentTaskService = studentTaskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudentTasks()
    {
        var studentTasks =
            await _studentTaskService
                .GetAllAsync();

        return Ok(studentTasks);
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetStudentTasks(
        int studentId
    )
    {
        var studentTasks =
            await _studentTaskService
                .GetByStudentIdAsync(studentId);

        return Ok(studentTasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentTaskById(
        int id
    )
    {
        var studentTask =
            await _studentTaskService
                .GetByIdAsync(id);

        if (studentTask is null)
        {
            return NotFound(
                "Student task not found."
            );
        }

        return Ok(studentTask);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudentTask(
        CreateStudentTaskDto dto
    )
    {
        try
        {
            var studentTask =
                await _studentTaskService
                    .CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetStudentTaskById),
                new
                {
                    id = studentTask.StudentTaskId
                },
                studentTask
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
    public async Task<IActionResult> UpdateStudentTask(
        int id,
        UpdateStudentTaskDto dto
    )
    {
        var studentTask =
            await _studentTaskService
                .UpdateAsync(id, dto);

        if (studentTask is null)
        {
            return NotFound(
                "Student task not found."
            );
        }

        return Ok(studentTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudentTask(
        int id
    )
    {
        var deleted =
            await _studentTaskService
                .DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(
                "Student task not found."
            );
        }

        return NoContent();
    }

    
}