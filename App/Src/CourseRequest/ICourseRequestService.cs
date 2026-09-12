using BackEndCodeTrix.Src.CourseRequest.CourseRequestDTO;

namespace BackEndCodeTrix.Src.CourseRequest;

public interface ICourseRequestService
{
    Task<CourseRequestResponseDto> CreateAsync(
        CreateCourseRequestDto dto);

    Task<List<CourseRequestResponseDto>> GetAllAsync();

    Task<CourseRequestResponseDto> GetByIdAsync(int id);

    Task<CourseRequestResponseDto> UpdateAsync(
        int id,
        UpdateCourseRequestDto dto);

    Task DeleteAsync(int id);

    Task<CourseRequestResponseDto> ChangeStatusAsync(
        int id,
        ChangeCourseRequestStatusDto dto);
}