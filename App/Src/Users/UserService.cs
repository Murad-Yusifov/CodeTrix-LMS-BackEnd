using AutoMapper;
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

    public async Task<UserResponseDto?> UpdateAsync(
        int id,
        CreateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return null;
        }

        _mapper.Map(dto, user);

        var updatedUser = await _userRepository
            .UpdateAsync(user);

        return _mapper.Map<UserResponseDto>(updatedUser);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }
}
