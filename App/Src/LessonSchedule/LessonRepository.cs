using BackEndCodeTrix.Src.Data;
using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.LessonSchedule;
using Microsoft.EntityFrameworkCore;

namespace BackEndCodeTrix.Src.Lesson;

public class LessonRepository : ILessonRepository
{
    private readonly ApplicationDbContext _context;

    public LessonRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET ALL
    public async Task<List<LessonModel>> GetAllLessonsAsync()
    {
        return await _context.Lessons
            .AsNoTracking()
            .Include(l => l.Group)
            .ToListAsync();
    }

    // GET BY ID
    public async Task<LessonModel?> GetSingleLessonByIdAsync(int id)
    {
        return await _context.Lessons
            .AsNoTracking()
            .Include(l => l.Group)
            .FirstOrDefaultAsync(
                lesson => lesson.LessonId == id
            );
    }

    // CREATE LESSON
    public async Task<LessonModel> CreateLessonAsync(
        LessonModel lesson
    )
    {
        _context.Lessons.Add(lesson);

        await _context.SaveChangesAsync();

        return lesson;
    }

    // CREATE ATTENDANCE FOR LESSON
    public async Task CreateAttendanceForLessonAsync(
        int lessonId,
        int groupId
    )
    {
        // Find all students in the group
        var students = await _context.Users
            .Where(u =>
                u.GroupId == groupId &&
                u.RoleId == 1
            )
            .ToListAsync();

        // Create attendance
        var attendance = new AttendanceModel
        {
            LessonId = lessonId,
            GroupId = groupId,
            RecordedAt = DateTime.UtcNow
        };

        _context.StudentAttendances.Add(attendance);

        await _context.SaveChangesAsync();

        // Add every student
        foreach (var student in students)
        {
            _context.AttendanceStudents.Add(
                new AttendanceStudentModel
                {
                    AttendanceId = attendance.AttendanceId,
                    StudentId = student.UserId,
                    Status = false
                }
            );
        }

        await _context.SaveChangesAsync();
    }

    // UPDATE
    public async Task<LessonModel?> UpdateLessonAsync(
        int id,
        LessonModel lesson
    )
    {
        var existingLesson = await _context.Lessons
            .FirstOrDefaultAsync(
                l => l.LessonId == id
            );

        if (existingLesson is null)
        {
            return null;
        }

        existingLesson.GroupId = lesson.GroupId;
        existingLesson.LessonStarts = lesson.LessonStarts;
        existingLesson.LessonEnds = lesson.LessonEnds;
        existingLesson.LocationType = lesson.LocationType;
        existingLesson.Classroom = lesson.Classroom;

        await _context.SaveChangesAsync();

        return existingLesson;
    }

    // DELETE
    public async Task<bool> DeleteLessonAsync(int id)
    {
        var lesson = await _context.Lessons
            .FirstOrDefaultAsync(
                l => l.LessonId == id
            );

        if (lesson is null)
        {
            return false;
        }

        _context.Lessons.Remove(lesson);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<GroupModel?> GetGroupByLessonIdAsync(
        int lessonId
    )
    {
        return await _context.Lessons
            .AsNoTracking()
            .Where(l => l.LessonId == lessonId)
            .Include(l => l.Group)
                .ThenInclude(g => g.CreatedByMentor)
            .Select(l => l.Group)
            .FirstOrDefaultAsync();
    }
}