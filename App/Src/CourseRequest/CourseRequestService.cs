using AutoMapper;
using BackEndCodeTrix.Src.CourseRequest.CourseRequestDTO;

namespace BackEndCodeTrix.Src.CourseRequest;

public class CourseRequestService : ICourseRequestService
{
    private readonly ICourseRequestRepository _repository;
    private readonly IMapper _mapper;

    public CourseRequestService(
        ICourseRequestRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CourseRequestResponseDto> CreateAsync(
        CreateCourseRequestDto dto)
    {
        var courseExists =
            await _repository.CourseExistsAsync(dto.CourseId);

        if (!courseExists)
        {
            throw new KeyNotFoundException(
                $"Course with id {dto.CourseId} was not found.");
        }

        var request = _mapper.Map<CourseRequestModel>(dto);

        request.Status = CourseRequestStatus.Pending;
        request.CreatedAt = DateTime.UtcNow;

        var createdRequest =
            await _repository.CreateAsync(request);

        return _mapper.Map<CourseRequestResponseDto>(
            createdRequest);
    }

    public async Task<List<CourseRequestResponseDto>> GetAllAsync()
    {
        var requests = await _repository.GetAllAsync();

        return _mapper.Map<List<CourseRequestResponseDto>>(
            requests);
    }

    public async Task<CourseRequestResponseDto> GetByIdAsync(int id)
    {
        var request = await _repository.GetByIdAsync(id);

        if (request == null)
        {
            throw new KeyNotFoundException(
                $"Course request with id {id} was not found.");
        }

        return _mapper.Map<CourseRequestResponseDto>(request);
    }

    public async Task<CourseRequestResponseDto> UpdateAsync(
        int id,
        UpdateCourseRequestDto dto)
    {
        var request = await _repository.GetByIdAsync(id);

        if (request == null)
        {
            throw new KeyNotFoundException(
                $"Course request with id {id} was not found.");
        }

        var courseExists =
            await _repository.CourseExistsAsync(dto.CourseId);

        if (!courseExists)
        {
            throw new KeyNotFoundException(
                $"Course with id {dto.CourseId} was not found.");
        }

        _mapper.Map(dto, request);

        request.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(request);

        return _mapper.Map<CourseRequestResponseDto>(
            request);
    }

    public async Task DeleteAsync(int id)
    {
        var request = await _repository.GetByIdAsync(id);

        if (request == null)
        {
            throw new KeyNotFoundException(
                $"Course request with id {id} was not found.");
        }

        await _repository.DeleteAsync(request);
    }

    public async Task<CourseRequestResponseDto> ChangeStatusAsync(
        int id,
        ChangeCourseRequestStatusDto dto)
    {
        var request = await _repository.GetByIdAsync(id);

        if (request == null)
        {
            throw new KeyNotFoundException(
                $"Course request with id {id} was not found.");
        }

        request.Status = dto.Status;
        request.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(request);

        return _mapper.Map<CourseRequestResponseDto>(
            request);
    }
}