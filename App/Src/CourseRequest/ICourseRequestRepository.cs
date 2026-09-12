namespace BackEndCodeTrix.Src.CourseRequest;

public interface ICourseRequestRepository
{
    Task<CourseRequestModel> CreateAsync(CourseRequestModel request);

    Task<List<CourseRequestModel>> GetAllAsync();

    Task<CourseRequestModel?> GetByIdAsync(int id);

    Task UpdateAsync(CourseRequestModel request);

    Task DeleteAsync(CourseRequestModel request);

    Task<bool> CourseExistsAsync(int courseId);
}