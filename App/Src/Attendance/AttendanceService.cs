using AutoMapper;
using BackEndCodeTrix.Src.Attendance;
using BackEndCodeTrix.Src.Attendance.AttendanceDTO;
using BackEndCodeTrix.Src.LessonSchedule.AttendanceDTO;

namespace BackEndCodeTrix.Src.LessonSchedule;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IMapper _mapper;

    public AttendanceService(
        IAttendanceRepository attendanceRepository,
        IMapper mapper)
    {
        _attendanceRepository = attendanceRepository;
        _mapper = mapper;
    }

    public async Task<List<AttendanceResponseDto>> GetAllAsync()
    {
        var attendances =
            await _attendanceRepository.GetAllAsync();

        return _mapper.Map<List<AttendanceResponseDto>>(
            attendances);
    }

    public async Task<AttendanceResponseDto?> GetByIdAsync(int id)
    {
        var attendance =
            await _attendanceRepository.GetByIdAsync(id);

        if (attendance is null)
        {
            return null;
        }

        return _mapper.Map<AttendanceResponseDto>(
            attendance);
    }

    public async Task<List<AttendanceResponseDto>?> GetByStudentIdAsync(
        int studentId)
    {
        if (!await _attendanceRepository.StudentExistsAsync(studentId))
        {
            return null;
        }

        var attendances =
            await _attendanceRepository
                .GetByStudentIdAsync(studentId);

        return _mapper.Map<List<AttendanceResponseDto>>(
            attendances);
    }

    public async Task<List<AttendanceResponseDto>?> GetByLessonIdAsync(
        int lessonId)
    {
        if (!await _attendanceRepository.LessonExistsAsync(lessonId))
        {
            return null;
        }

        var attendances =
            await _attendanceRepository
                .GetByLessonIdAsync(lessonId);

        return _mapper.Map<List<AttendanceResponseDto>>(
            attendances);
    }

    public async Task<AttendanceResponseDto?> CreateAsync(
        CreateAttendanceDto dto)
    {
        var studentExists =
            await _attendanceRepository
                .StudentExistsAsync(dto.StudentId);

        if (!studentExists)
        {
            return null;
        }

        var lessonExists =
            await _attendanceRepository
                .LessonExistsAsync(dto.LessonId);

        if (!lessonExists)
        {
            return null;
        }

        var alreadyExists =
            await _attendanceRepository
                .AttendanceExistsAsync(
                    dto.StudentId,
                    dto.LessonId);

        if (alreadyExists)
        {
            return null;
        }

        var attendance =
            _mapper.Map<AttendanceModel>(dto);

        attendance.RecordedAt = DateTime.UtcNow;

        var created =
            await _attendanceRepository
                .CreateAsync(attendance);

        return _mapper.Map<AttendanceResponseDto>(
            created);
    }

    public async Task<AttendanceResponseDto?> UpdateAsync(
        int id,
        UpdateAttendanceDto dto)
    {
        var attendance =
            await _attendanceRepository.GetByIdAsync(id);

        if (attendance is null)
        {
            return null;
        }

        attendance.Status = dto.Status;
        attendance.RecordedAt = DateTime.UtcNow;

        var updated =
            await _attendanceRepository
                .UpdateAsync(attendance);

        if (updated is null)
        {
            return null;
        }

        return _mapper.Map<AttendanceResponseDto>(
            updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _attendanceRepository.DeleteAsync(id);
    }
}