using BackEndCodeTrix.Src.Tasks.TaskDTO;

namespace BackEndCodeTrix.Src.Tasks;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(
        ITaskRepository taskRepository
    )
    {
        _taskRepository = taskRepository;
    }

    public async Task<List<TaskResponseDto>> GetAllAsync()
    {
        var tasks = await _taskRepository
            .GetAllAsync();

        return tasks
            .Select(task => new TaskResponseDto
            {
                TaskId = task.TaskId,
                TaskName = task.TaskName,
                DeadLine = task.DeadLine,
                DateTime = task.DateTime,
                TaskStatus = task.TaskStatus,
                MentorComment = task.MentorComment,
                GroupId = task.GroupId
            })
            .ToList();
    }

    public async Task<TaskResponseDto?> GetByIdAsync(
        int id
    )
    {
        var task = await _taskRepository
            .GetByIdAsync(id);

        if (task is null)
        {
            return null;
        }

        return new TaskResponseDto
        {
            TaskId = task.TaskId,
            TaskName = task.TaskName,
            DeadLine = task.DeadLine,
            DateTime = task.DateTime,
            TaskStatus = task.TaskStatus,
            MentorComment = task.MentorComment,
            GroupId = task.GroupId
        };
    }

    public async Task<TaskResponseDto> CreateAsync(
        CreateTaskDto dto
    )
    {
        var task = new TaskModel
        {
            TaskName = dto.TaskName,
            DeadLine = dto.DeadLine,
            DateTime = dto.DateTime,
            GroupId = dto.GroupId,
            MentorComment = dto.MentorComment,
            TaskStatus = "NotAssigned"
        };

        var createdTask = await _taskRepository
            .CreateAsync(task);

        return new TaskResponseDto
        {
            TaskId = createdTask.TaskId,
            TaskName = createdTask.TaskName,
            DeadLine = createdTask.DeadLine,
            DateTime = createdTask.DateTime,
            TaskStatus = createdTask.TaskStatus,
            MentorComment = createdTask.MentorComment,
            GroupId = createdTask.GroupId
        };
    }

    public async Task<TaskResponseDto?> UpdateAsync(
        int id,
        UpdateTaskDto dto
    )
    {
        var task = new TaskModel
        {
            TaskName = dto.TaskName,
            DeadLine = dto.DeadLine,
            DateTime = dto.DateTime,
            MentorComment = dto.MentorComment
        };

        var updatedTask = await _taskRepository
            .UpdateAsync(id, task);

        if (updatedTask is null)
        {
            return null;
        }

        return new TaskResponseDto
        {
            TaskId = updatedTask.TaskId,
            TaskName = updatedTask.TaskName,
            DeadLine = updatedTask.DeadLine,
            DateTime = updatedTask.DateTime,
            TaskStatus = updatedTask.TaskStatus,
            MentorComment = updatedTask.MentorComment,
            GroupId = updatedTask.GroupId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _taskRepository
            .DeleteAsync(id);
    }
}