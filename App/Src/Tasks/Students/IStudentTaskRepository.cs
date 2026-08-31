namespace BackEndCodeTrix.Src.Tasks.Students;

public interface IStudentTaskRepository
{
    Task<List<StudentTaskModel>> GetAllAsync();

    Task<List<StudentTaskModel>> GetByStudentIdAsync(
        int studentId
    );

    Task<StudentTaskModel?> GetByIdAsync(
        int id
    );

    Task<StudentTaskModel?> GetByStudentAndTaskAsync(
        int studentId,
        int taskId
    );

    Task<StudentTaskModel> CreateAsync(
        StudentTaskModel studentTask
    );

    Task<StudentTaskModel?> UpdateAsync(
        int id,
        StudentTaskModel studentTask
    );

    Task<bool> DeleteAsync(
        int id
    );
}