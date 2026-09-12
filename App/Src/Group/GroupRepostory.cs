using Microsoft.EntityFrameworkCore;
using BackEndCodeTrix.Src.Data;
using BackEndCodeTrix.Src.Users;
using BackEndCodeTrix.Src.Group.GroupDTO;
namespace BackEndCodeTrix.Src.Group;

public class GroupRepository : IGroupRepository
{
    private readonly ApplicationDbContext _context;

    public GroupRepository(
        ApplicationDbContext context
    )
    {
        _context = context;
    }

    public async Task<List<GroupModel>> GetAllAsync()
    {
        return await _context.Groups
            .Include(group => group.CreatedByMentor)
            .Include(g => g.Course)
            .Include(group => group.Students)
            .Include(group => group.Tasks)
            .Include(group => group.Lessons)
            .ToListAsync();
    }

    public async Task<GroupModel?> GetByIdAsync(int id)
    {
        return await _context.Groups
            .Include(group => group.CreatedByMentor)
            .Include(group => group.Students)
            .Include(group => group.Tasks)
            .Include(group => group.Lessons)
            .Include(group => group.Course)
            .FirstOrDefaultAsync(
                group => group.GroupId == id
            );
    }

  public async Task<GroupModel> CreateAsync(GroupModel group)
{
    _context.Groups.Add(group);

    await _context.SaveChangesAsync();

    return await _context.Groups
        .Include(g => g.Course)
        .Include(g => g.CreatedByMentor)
        .Include(g => g.Students)
        .Include(g => g.Tasks)
        .Include(g => g.Lessons)
        .FirstAsync(g => g.GroupId == group.GroupId);
}

    public async Task<GroupModel> UpdateAsync(
        GroupModel group
    )
    {
        _context.Groups.Update(group);

        await _context.SaveChangesAsync();

        return group;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var group = await _context.Groups
            .FirstOrDefaultAsync(
                group => group.GroupId == id
            );

        if (group is null)
        {
            return false;
        }

        _context.Groups.Remove(group);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> MentorExistsAsync(
        int mentorId
    )
    {
        return await _context.Users
            .AnyAsync(user =>
                user.UserId == mentorId &&
                user.Role.RoleName == "Mentor"
            );
    }

    public async Task<List<UserModel>?> GetStudentsByGroupId(int groupId)
    {
        return await _context.Users.
  AsNoTracking()
  .Where(st => st.GroupId == groupId)
  .ToListAsync();
    }
}