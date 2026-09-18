using AutoMapper;
using BackEndCodeTrix.Src.Group.GroupDTO;
using BackEndCodeTrix.Src.Tasks.StudentTaskDTO;
using BackEndCodeTrix.Src.Users.UserDTO;

namespace BackEndCodeTrix.Src.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<AttendanceResponseDto>>
    GetStudentAttendanceAsync(int userId)
{
    var attendances =
        await _userRepository
            .GetStudentAttendanceAsync(userId);

    return _mapper.Map<List<AttendanceResponseDto>>(
        attendances
    );
}

    public async Task<List<UserResponseDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<List<UserResponseDto>>(users);
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return null;
        }

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
    {
        var existingUser = await _userRepository
            .GetByEmailAsync(dto.Email);

        if (existingUser is not null)
        {
            throw new Exception("User with this email already exists.");
        }

        var user = _mapper.Map<UserModel>(dto);

        // Temporary for now
        // We will add BCrypt later
        user.PasswordHash = dto.Password;

        var createdUser = await _userRepository
            .CreateAsync(user);

        return _mapper.Map<UserResponseDto>(createdUser);
    }

    public async Task<(UserResponseDto? User, string? Error)> UpdateAsync(
    int id,
    UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
            return (null, "UserNotFound");

        _mapper.Map(dto, user);

        var updatedUser = await _userRepository.UpdateAsync(user);

        if (updatedUser is null)
            return (null, "GroupNotFound");

        return (_mapper.Map<UserResponseDto>(updatedUser), null);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    //  public async Task<List<UserResponseDto>> GetByGroupIdAsync(int id)
    // {
    //     var users = await _userRepository.GetByGroupIdAsync(id);

    //     return _mapper.Map<List<UserResponseDto>>(users);
    // }
    public async Task<GroupResponseDto?> GetGroupByUserIdAsync(int userId)
    {
        var group = await _userRepository.GetGroupByUserIdAsync(userId);

        if (group is null)
            return null;

        return _mapper.Map<GroupResponseDto>(group);
    }

    public async Task<List<StudentTaskResponseDto>> GetAllStudentTasksByStudentIdAsync(int userId)
    {
        var studentTasks = await _userRepository.GetAllStudentTasksByStudentIdAsync(userId);

        return _mapper.Map<List<StudentTaskResponseDto>>(
            studentTasks
        );
    }
    public async Task<List<AttendanceResponseDto>> GetStudentAttendanceByStudentId (int studentId)
    {
        var  attendance = await _userRepository.GetStudentAttendanceAsync(studentId);
        
        return _mapper.Map<List<AttendanceResponseDto>>(attendance);
    }
}
