using Microsoft.EntityFrameworkCore;
using BackEndCodeTrix.Src.Data;
using BackEndCodeTrix.Src.Group.GroupDTO;
using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Users;

namespace BackEndCodeTrix.Src.Tasks;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(
        ApplicationDbContext context
    )
    {
        _context = context;
    }

    public async Task<List<TaskModel>> GetAllAsync()
    {
        return await _context.Tasks
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TaskModel?> GetByIdAsync(int id)
    {
        return await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(task => task.TaskId == id);
    }

    public async Task<TaskModel> CreateAsync(
        TaskModel task
    )
    {
        _context.Tasks.Add(task);

        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<TaskModel?> UpdateAsync(
        int id,
        TaskModel task
    )
    {
        var existingTask = await _context.Tasks
            .FirstOrDefaultAsync(
                item => item.TaskId == id
            );

        if (existingTask is null)
        {
            return null;
        }

        existingTask.TaskName = task.TaskName;
        existingTask.DeadLine = task.DeadLine;
        existingTask.DateTime = task.DateTime;
        existingTask.MentorComment = task.MentorComment;

        await _context.SaveChangesAsync();

        return existingTask;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(
                item => item.TaskId == id
            );

        if (task is null)
        {
            return false;
        }

        _context.Tasks.Remove(task);

        await _context.SaveChangesAsync();

        return true;
    }

  public async Task<List<UserModel>> GetStudentsByTaskIdAsync(int taskId)
{
    return await _context.Tasks
        .Where(t => t.TaskId == taskId)
        .SelectMany(t => t.Group.Students)
        .ToListAsync();
}

    public async Task<GroupModel?> GetGroupByTaksIdAsync(int taskId)
    {
        return await _context.Tasks
        .Where(t => t.TaskId == taskId)

        .Include(t => t.Group)
            .ThenInclude(g => g.CreatedByMentor)

        .Include(t => t.Group)
            .ThenInclude(g => g.Students)
        .Include(t => t.Group)
            .ThenInclude(g => g.Tasks)
        .Include(t => t.Group)
            .ThenInclude(g => g.Lessons)
        .Select(t => t.Group)
        .FirstOrDefaultAsync();

    }
}