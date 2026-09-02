using AutoMapper;
using BackEndCodeTrix.Src.Group.GroupDTO;
using BackEndCodeTrix.Src.Tasks.TaskDTO;
using SQLitePCL;

namespace BackEndCodeTrix.Src.Tasks;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public TaskService(
        ITaskRepository taskRepository,
        IMapper mapper
    )
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<List<TaskResponseDto>> GetAllAsync()
    {
        var tasks = await _taskRepository
            .GetAllAsync();

        return _mapper.Map<List<TaskResponseDto>>(tasks);
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

        return _mapper.Map<TaskResponseDto>(task);
    }

    public async Task<TaskResponseDto> CreateAsync(
        CreateTaskDto dto
    )
    {
        var task = _mapper.Map<TaskModel>(dto);

            task.TaskStatus="NotAssiigned";

        var createdTask = await _taskRepository
            .CreateAsync(task);

        return _mapper.Map<TaskResponseDto>(createdTask);
    }

    public async Task<TaskResponseDto?> UpdateAsync(
        int id,
        UpdateTaskDto dto
    )
    {
        var task = _mapper.Map<TaskModel>(dto);

        var updatedTask = await _taskRepository
            .UpdateAsync(id, task);

        if (updatedTask is null)
        {
            return null;
        }

        return _mapper.Map<TaskResponseDto>(updatedTask);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _taskRepository
            .DeleteAsync(id);
    }

    public async Task<GroupResponseDto?> GetGroupByTaskIdAsync(int taskId)
    {
        var group = await _taskRepository.GetGroupByTaksIdAsync(taskId);
        if(group is null)
        {
            return null;
        }

        return  _mapper.Map<GroupResponseDto>(group);
    }
}