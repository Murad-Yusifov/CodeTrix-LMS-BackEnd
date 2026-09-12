using BackEndCodeTrix.Src.Course.CourseDTO;

namespace BackEndCodeTrix.Src.Course;

public interface ICourseService
{
    Task<List<CourseResponseDto>> GetAllAsync();

    Task<CourseResponseDto?> GetByIdAsync(int id);

    Task<CourseResponseDto?> GetBySlugAsync(string slug);

    Task<CourseResponseDto> CreateAsync(CreateCourseDto dto);

    Task<CourseResponseDto?> UpdateAsync(int id, UpdateCourseDto dto);

    Task<bool> DeleteAsync(int id);

    Task<CourseResponseDto?> UpdateStatusAsync(
        int id,
        UpdateCourseStatusDto dto);
}