using Microsoft.EntityFrameworkCore;
using BackEndCodeTrix.Src.Data;
using BackEndCodeTrix.Src.Tasks.Students;

namespace BackEndCodeTrix.Src.Tasks;

public class StudentTaskRepository : IStudentTaskRepository
{
    private readonly ApplicationDbContext _context;

    public StudentTaskRepository(
        ApplicationDbContext context
    )
    {
        _context = context;
    }

    public async Task<List<StudentTaskModel>> GetAllAsync()
    {
        return await _context.StudentTasks
            .AsNoTracking()
            .Include(st => st.Task)
            .Include(st => st.Student)
            .ToListAsync();
    }

    public async Task<List<StudentTaskModel>> GetByStudentIdAsync(
        int studentId
    )
    {
        return await _context.StudentTasks
            .AsNoTracking()
            .Include(st => st.Task)
            .Where(st => st.StudentId == studentId)
            .ToListAsync();
    }

    public async Task<StudentTaskModel?> GetByIdAsync(
        int id
    )
    {
        return await _context.StudentTasks
            .AsNoTracking()
            .Include(st => st.Task)
            .Include(st => st.Student)
            .FirstOrDefaultAsync(
                st => st.StudentTaskId == id
            );
    }

    public async Task<StudentTaskModel?> GetByStudentAndTaskAsync(
        int studentId,
        int taskId
    )
    {
        return await _context.StudentTasks
            .FirstOrDefaultAsync(
                st =>
                    st.StudentId == studentId &&
                    st.TaskId == taskId
            );
    }

    public async Task<StudentTaskModel> CreateAsync(
        StudentTaskModel studentTask
    )
    {
        _context.StudentTasks.Add(studentTask);

        await _context.SaveChangesAsync();

        return studentTask;
    }

    public async Task<StudentTaskModel?> UpdateAsync(
        int id,
        StudentTaskModel studentTask
    )
    {
        var existingStudentTask =
            await _context.StudentTasks
                .FirstOrDefaultAsync(
                    st => st.StudentTaskId == id
                );

        if (existingStudentTask is null)
        {
            return null;
        }

        existingStudentTask.Status =
            studentTask.Status;

        // existingStudentTask.StudentComment =
        //     studentTask.StudentComment;

        existingStudentTask.CompletedAt =
            studentTask.CompletedAt;

        existingStudentTask.MentorComment =
            studentTask.MentorComment;

        await _context.SaveChangesAsync();

        return existingStudentTask;
    }

    public async Task<bool> DeleteAsync(
        int id
    )
    {
        var studentTask =
            await _context.StudentTasks
                .FirstOrDefaultAsync(
                    st => st.StudentTaskId == id
                );

        if (studentTask is null)
        {
            return false;
        }

        _context.StudentTasks.Remove(studentTask);

        await _context.SaveChangesAsync();

        return true;
    }
}