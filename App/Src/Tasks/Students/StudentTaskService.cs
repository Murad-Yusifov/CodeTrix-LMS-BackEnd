using AutoMapper;
using BackEndCodeTrix.Src.Tasks.Students;
using BackEndCodeTrix.Src.Tasks.StudentTaskDTO;

namespace BackEndCodeTrix.Src.Tasks;

public class StudentTaskService : IStudentTaskService
{
    private readonly IStudentTaskRepository _studentTaskRepository;
    private readonly IMapper _mapper;

    public StudentTaskService(
        IStudentTaskRepository studentTaskRepository,
        IMapper mapper
    )
    {
        _studentTaskRepository = studentTaskRepository;
        _mapper = mapper;
    }

    public async Task<List<StudentTaskResponseDto>> GetAllAsync()
    {
        var studentTasks =
            await _studentTaskRepository
                .GetAllAsync();

        return _mapper
            .Map<List<StudentTaskResponseDto>>(
                studentTasks
            );
    }

    public async Task<List<StudentTaskResponseDto>>
        GetByStudentIdAsync(int studentId)
    {
        var studentTasks =
            await _studentTaskRepository
                .GetByStudentIdAsync(studentId);

        return _mapper
            .Map<List<StudentTaskResponseDto>>(
                studentTasks
            );
    }

    public async Task<StudentTaskResponseDto?>
        GetByIdAsync(int id)
    {
        var studentTask =
            await _studentTaskRepository
                .GetByIdAsync(id);

        if (studentTask is null)
        {
            return null;
        }

        return _mapper
            .Map<StudentTaskResponseDto>(
                studentTask
            );
    }

    public async Task<StudentTaskResponseDto>
        CreateAsync(CreateStudentTaskDto dto)
    {
        var existing =
            await _studentTaskRepository
                .GetByStudentAndTaskAsync(
                    dto.StudentId,
                    dto.TaskId
                );

        if (existing is not null)
        {
            throw new Exception(
                "This task is already assigned to this student."
            );
        }

        var studentTask =
            _mapper.Map<StudentTaskModel>(dto);

        studentTask.Status = StudentTaskStatus.Pending;

        var createdStudentTask =
            await _studentTaskRepository
                .CreateAsync(studentTask);

        return _mapper
            .Map<StudentTaskResponseDto>(
                createdStudentTask
            );
    }

    public async Task<StudentTaskResponseDto?>
        UpdateAsync(
            int id,
            UpdateStudentTaskDto dto
        )
    {
        var studentTask =
            await _studentTaskRepository
                .GetByIdAsync(id);

        if (studentTask is null)
        {
            return null;
        }

        studentTask.Status = dto.Status;
        // studentTask.StudentComment =
        //     dto.StudentComment;

        if (dto.Status == StudentTaskStatus.Completed)
        {
            studentTask.CompletedAt =
                DateTime.UtcNow;
        }
        else
        {
            studentTask.CompletedAt = null;
        }

        var updatedStudentTask =
            await _studentTaskRepository
                .UpdateAsync(
                    id,
                    studentTask
                );

        return _mapper
            .Map<StudentTaskResponseDto>(
                updatedStudentTask
            );
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _studentTaskRepository
            .DeleteAsync(id);
    }

   
}