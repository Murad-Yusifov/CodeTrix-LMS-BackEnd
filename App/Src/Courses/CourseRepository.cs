using BackEndCodeTrix.Src.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEndCodeTrix.Src.Course;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CourseModel>> GetAllAsync()
    {
        return await _context.Courses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CourseModel?> GetByIdAsync(int id)
    {
        return await _context.Courses
            .FirstOrDefaultAsync(c => c.CourseId == id);
    }

    public async Task<CourseModel?> GetBySlugAsync(string slug)
    {
        return await _context.Courses
            .FirstOrDefaultAsync(c => c.Slug == slug);
    }

    public async Task<CourseModel> CreateAsync(CourseModel course)
    {
        _context.Courses.Add(course);

        await _context.SaveChangesAsync();

        return course;
    }

    public async Task UpdateAsync(CourseModel course)
    {
        _context.Courses.Update(course);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CourseModel course)
    {
        _context.Courses.Remove(course);

        await _context.SaveChangesAsync();
    }
}