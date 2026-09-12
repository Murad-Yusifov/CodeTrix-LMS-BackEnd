using BackEndCodeTrix.Src.Data;
using Microsoft.EntityFrameworkCore;
using BackEndCodeTrix.Src.CourseRequest;

namespace BackEndCodeTrix.Src.CourseRequest;

public class CourseRequestRepository : ICourseRequestRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CourseRequestModel> CreateAsync(
        CourseRequestModel request)
    {
        await _context.CourseRequests.AddAsync(request);
        await _context.SaveChangesAsync();

        return request;
    }

    public async Task<List<CourseRequestModel>> GetAllAsync()
    {
        return await _context.CourseRequests
            .Include(x => x.Course)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<CourseRequestModel?> GetByIdAsync(int id)
    {
        return await _context.CourseRequests
            .Include(x => x.Course)
            .FirstOrDefaultAsync(x => x.CourseRequestId == id);
    }

    public async Task UpdateAsync(CourseRequestModel request)
    {
        _context.CourseRequests.Update(request);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CourseRequestModel request)
    {
        _context.CourseRequests.Remove(request);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CourseExistsAsync(int courseId)
    {
        return await _context.Courses
            .AnyAsync(x => x.CourseId == courseId);
    }
}