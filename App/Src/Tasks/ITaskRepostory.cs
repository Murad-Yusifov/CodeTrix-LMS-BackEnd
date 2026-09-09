using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Group.GroupDTO;
using BackEndCodeTrix.Src.Users;

namespace BackEndCodeTrix.Src.Tasks;

public interface ITaskRepository
{
    Task<List<TaskModel>> GetAllAsync();

    Task<TaskModel?> GetByIdAsync(int id);

    Task<TaskModel> CreateAsync(TaskModel task);

    Task<TaskModel?> UpdateAsync(
        int id,
        TaskModel task
    );

    Task<bool> DeleteAsync(int id);
    Task<GroupModel?> GetGroupByTaksIdAsync(int taskId);
    Task<List<UserModel>> GetStudentsByTaskIdAsync (int taskId);
}