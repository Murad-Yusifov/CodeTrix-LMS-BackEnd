using Microsoft.EntityFrameworkCore;
using BackEndCodeTrix.Src.Data;
using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Tasks;
using BackEndCodeTrix.Src.LessonSchedule;

namespace BackEndCodeTrix.Src.Users;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserModel>> GetAllAsync()
    {
        var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == "whateverStudent3@codetrix.com");

        Console.WriteLine($"Email: {user?.Email}");
        Console.WriteLine($"PasswordHash: {user?.PasswordHash}");
        return await _context.Users
            .Include(user => user.Role)
            .Include(user => user.Group)
            // .Include(attendance =>attendance.Attendance)
            .ToListAsync();
    }

    public async Task<UserModel?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(user => user.Role)
            .Include(user => user.Group)
            .FirstOrDefaultAsync(user => user.UserId == id);
    }

    public async Task<UserModel?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<UserModel> CreateAsync(UserModel user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<UserModel?> UpdateAsync(UserModel user)
    {
        var groupExists = await _context.Groups
     .AnyAsync(g => g.GroupId == user.GroupId);

        if (!groupExists)
        {
            return null;
        }

        _context.Users.Update(user);


        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.UserId == id);

        if (user is null)
        {
            return false;
        }

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return true;
    }

    // public async Task<List<UserModel>> GetByGroupIdAsync(int groupId)
    // {
    //     return await _context.Users
    //         .Include(user => user.Role)
    //         .Include(user => user.Group)
    //         .Where(user => user.GroupId == groupId)
    //         .ToListAsync();
    // }

    public async Task<GroupModel?> GetGroupByUserIdAsync(int userId)
    {
        return await _context.Groups
            .Include(g => g.CreatedByMentor)
            .FirstOrDefaultAsync(g =>
                g.Students.Any(s => s.UserId == userId));
    }

    public async Task<List<StudentTaskModel>?> GetAllStudentTasksByStudentIdAsync(
        int userId
    )
    {
        var studentExists = await _context.Users
            .AnyAsync(u => u.UserId == userId);

        if (!studentExists)
        {
            return null;
        }

        return await _context.StudentTasks
            .AsNoTracking()
            .Where(st => st.StudentId == userId)
            .Include(st => st.Task)
            .Include(st => st.Student)
            .ToListAsync();
    }

    public async Task<List<AttendanceModel>> GetStudentAttendanceAsync(int userId)
    {
        return await _context.StudentAttendances
        .AsNoTracking()
        .Where(a => a.Students.Any(s => s.StudentId == userId))
        .Include(a => a.Students)
            .ThenInclude(s => s.Student)
        .Include(a => a.Lesson)
        .ToListAsync();


    }
}
