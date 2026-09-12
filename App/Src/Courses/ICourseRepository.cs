using BackEndCodeTrix.Src.Course;

namespace BackEndCodeTrix.Src.Course;

public interface ICourseRepository
{
    Task<List<CourseModel>> GetAllAsync();

    Task<CourseModel?> GetByIdAsync(int id);

    Task<CourseModel?> GetBySlugAsync(string slug);

    Task<CourseModel> CreateAsync(CourseModel course);

    Task UpdateAsync(CourseModel course);

    Task DeleteAsync(CourseModel course);
}