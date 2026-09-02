using Microsoft.EntityFrameworkCore;
using BackEndCodeTrix.Src.Data;
using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Tasks;

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
        return await _context.Users
            .Include(user => user.Role)
            .Include(user => user.Group)
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

    public async Task<UserModel> UpdateAsync(UserModel user)
    {
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
        return await _context.Users
            .Where(u => u.UserId == userId)
            .Include(u => u.Group)
                .ThenInclude(g => g.CreatedByMentor)
            .Select(u => u.Group)
            .FirstOrDefaultAsync();
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
}
