using AutoMapper;
using BackEndCodeTrix.Src.Course.CourseDTO;

namespace BackEndCodeTrix.Src.Course;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repository;
    private readonly IMapper _mapper;

    public CourseService(
        ICourseRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CourseResponseDto>> GetAllAsync()
    {
        var courses = await _repository.GetAllAsync();

        return _mapper.Map<List<CourseResponseDto>>(courses);
    }

    public async Task<CourseResponseDto?> GetByIdAsync(int id)
    {
        var course = await _repository.GetByIdAsync(id);

        if (course == null)
            return null;

        return _mapper.Map<CourseResponseDto>(course);
    }

    public async Task<CourseResponseDto?> GetBySlugAsync(string slug)
    {
        var course = await _repository.GetBySlugAsync(slug);

        if (course == null)
            return null;

        return _mapper.Map<CourseResponseDto>(course);
    }

    public async Task<CourseResponseDto> CreateAsync(
        CreateCourseDto dto)
    {
        var existingCourse = await _repository.GetBySlugAsync(dto.Slug);

        if (existingCourse != null)
            throw new InvalidOperationException(
                "A course with this slug already exists.");

        var course = _mapper.Map<CourseModel>(dto);

        course.IsActive = true;
        course.CreatedAt = DateTime.UtcNow;
        course.UpdatedAt = DateTime.UtcNow;

        await _repository.CreateAsync(course);

        return _mapper.Map<CourseResponseDto>(course);
    }

    public async Task<CourseResponseDto?> UpdateAsync(
        int id,
        UpdateCourseDto dto)
    {
        var course = await _repository.GetByIdAsync(id);

        if (course == null)
            return null;

        var existingCourse = await _repository.GetBySlugAsync(dto.Slug);

        if (existingCourse != null &&
            existingCourse.CourseId != id)
        {
            throw new InvalidOperationException(
                "A course with this slug already exists.");
        }

        course.Title = dto.Title;
        course.Slug = dto.Slug;
        course.Description = dto.Description;
        course.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(course);

        return _mapper.Map<CourseResponseDto>(course);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var course = await _repository.GetByIdAsync(id);

        if (course == null)
            return false;

        await _repository.DeleteAsync(course);

        return true;
    }

    public async Task<CourseResponseDto?> UpdateStatusAsync(
        int id,
        UpdateCourseStatusDto dto)
    {
        var course = await _repository.GetByIdAsync(id);

        if (course == null)
            return null;

        course.IsActive = dto.IsActive;
        course.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(course);

        return _mapper.Map<CourseResponseDto>(course);
    }
}