using BackEndCodeTrix.Src.Attendance.AttendanceDTO;
using BackEndCodeTrix.Src.LessonSchedule.AttendanceDTO;

namespace BackEndCodeTrix.Src.LessonSchedule;

public interface IAttendanceService
{
    Task<List<AttendanceResponseDto>> GetAllAsync();

    Task<AttendanceResponseDto?> GetByIdAsync(int id);

    Task<List<AttendanceResponseDto>?> GetByStudentIdAsync(int studentId);

    Task<List<AttendanceResponseDto>?> GetByLessonIdAsync(int lessonId);

    Task<AttendanceResponseDto?> CreateAsync(
        CreateAttendanceDto dto);

    Task<AttendanceResponseDto?> UpdateAsync(
        int id,
        UpdateAttendanceDto dto);

    Task<bool> DeleteAsync(int id);
}