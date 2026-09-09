namespace BackEndCodeTrix.Src.LessonSchedule;

public interface IAttendanceRepository
{
    Task<List<AttendanceModel>> GetAllAsync();

    Task<AttendanceModel?> GetByIdAsync(int id);

    Task<List<AttendanceModel>> GetByStudentIdAsync(int studentId);

    Task<List<AttendanceModel>> GetByLessonIdAsync(int lessonId);

    Task<bool> StudentExistsAsync(int studentId);

    Task<bool> LessonExistsAsync(int lessonId);

    Task<bool> AttendanceExistsAsync(int studentId, int lessonId);

    Task<AttendanceModel> CreateAsync(AttendanceModel attendance);

    Task<AttendanceModel?> UpdateAsync(AttendanceModel attendance);

    Task<bool> DeleteAsync(int id);
}