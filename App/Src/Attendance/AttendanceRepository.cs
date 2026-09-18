using BackEndCodeTrix.Src.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEndCodeTrix.Src.LessonSchedule;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly ApplicationDbContext _context;

    public AttendanceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AttendanceModel>> GetAllAsync()
    {
        return await _context.StudentAttendances
            .AsNoTracking()
            .Include(a => a.Students)
                .ThenInclude(s => s.Student)
            .Include(a => a.Lesson)
            .Include(a => a.Group)
            .ToListAsync();
    }

    public async Task<AttendanceModel?> GetByIdAsync(int id)
    {
        return await _context.StudentAttendances
            .Include(a => a.Students)
                .ThenInclude(s => s.Student)
            .Include(a => a.Lesson)
            .Include(a => a.Group)
            .FirstOrDefaultAsync(a =>
                a.AttendanceId == id);
    }

    public async Task<List<AttendanceModel>> GetByStudentIdAsync(
        int studentId)
    {
        return await _context.StudentAttendances
            .AsNoTracking()
            .Where(a =>
                a.Students.Any(s =>
                    s.StudentId == studentId))
            .Include(a => a.Students)
                .ThenInclude(s => s.Student)
            .Include(a => a.Lesson)
            .Include(a => a.Group)
            .OrderByDescending(a => a.Lesson.LessonStarts)
            .ToListAsync();
    }

    public async Task<List<AttendanceModel>> GetByLessonIdAsync(
        int lessonId)
    {
        return await _context.StudentAttendances
            .AsNoTracking()
            .Where(a => a.LessonId == lessonId)
            .Include(a => a.Students)
                .ThenInclude(s => s.Student)
            .Include(a => a.Lesson)
            .Include(a => a.Group)
            .ToListAsync();
    }

    public async Task<bool> StudentExistsAsync(int studentId)
    {
        return await _context.Users
            .AnyAsync(u =>
                u.UserId == studentId &&
                u.RoleId == 1);
    }

    public async Task<bool> LessonExistsAsync(int lessonId)
    {
        return await _context.Lessons
            .AnyAsync(l =>
                l.LessonId == lessonId);
    }

    public async Task<bool> AttendanceExistsAsync(
        int studentId,
        int lessonId)
    {
        return await _context.StudentAttendances
            .AnyAsync(a =>
                a.LessonId == lessonId &&
                a.Students.Any(s =>
                    s.StudentId == studentId));
    }

    public async Task<AttendanceModel> CreateAsync(
        AttendanceModel attendance)
    {
        _context.StudentAttendances.Add(attendance);

        await _context.SaveChangesAsync();

        return attendance;
    }

    public async Task<AttendanceModel?> UpdateAsync(
        AttendanceModel attendance)
    {
        var existingAttendance =
            await _context.StudentAttendances
                .FirstOrDefaultAsync(a =>
                    a.AttendanceId ==
                    attendance.AttendanceId);

        if (existingAttendance is null)
        {
            return null;
        }

        existingAttendance.RecordedAt =
            attendance.RecordedAt;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(
            attendance.AttendanceId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var attendance =
            await _context.StudentAttendances
                .FirstOrDefaultAsync(a =>
                    a.AttendanceId == id);

        if (attendance is null)
        {
            return false;
        }

        _context.StudentAttendances.Remove(attendance);

        await _context.SaveChangesAsync();

        return true;
    }
}
