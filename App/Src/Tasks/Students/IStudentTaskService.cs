using BackEndCodeTrix.Src.Tasks.StudentTaskDTO;

namespace BackEndCodeTrix.Src.Tasks.Students;

public interface IStudentTaskService
{
    Task<List<StudentTaskResponseDto>> GetAllAsync();

    Task<List<StudentTaskResponseDto>> GetByStudentIdAsync(
        int studentId
    );

    Task<StudentTaskResponseDto?> GetByIdAsync(
        int id
    );

    Task<StudentTaskResponseDto> CreateAsync(
        CreateStudentTaskDto dto
    );

    Task<StudentTaskResponseDto?> UpdateAsync(
        int id,
        UpdateStudentTaskDto dto
    );

    Task<bool> DeleteAsync(
        int id
    );
}