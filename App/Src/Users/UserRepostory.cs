using Microsoft.EntityFrameworkCore;
using BackEndCodeTrix.Src.Data;

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
}
