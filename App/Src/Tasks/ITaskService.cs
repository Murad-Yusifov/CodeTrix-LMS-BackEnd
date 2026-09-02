using BackEndCodeTrix.Src.Group.GroupDTO;
using BackEndCodeTrix.Src.Tasks.TaskDTO;

namespace BackEndCodeTrix.Src.Tasks;

public interface ITaskService
{
    Task<List<TaskResponseDto>> GetAllAsync();

    Task<TaskResponseDto?> GetByIdAsync(int id);

    Task<TaskResponseDto> CreateAsync(
        CreateTaskDto dto
    );

    Task<TaskResponseDto?> UpdateAsync(
        int id,
        UpdateTaskDto dto
    );

    Task<bool> DeleteAsync(int id);
    Task<GroupResponseDto?> GetGroupByTaskIdAsync(int id);
}