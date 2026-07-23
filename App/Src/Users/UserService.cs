using BackEndCodeTrix.Src.Users.UserDTO;

namespace BackEndCodeTrix.Src.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponseDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(user => new UserResponseDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            UserSurName = user.UserSurName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            RoleId = user.RoleId,
            GroupId = user.GroupId
        }).ToList();
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return null;
        }

        return new UserResponseDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            UserSurName = user.UserSurName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            RoleId = user.RoleId,
            GroupId = user.GroupId
        };
    }

    public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
    {
        var existingUser = await _userRepository
            .GetByEmailAsync(dto.Email);

        if (existingUser is not null)
        {
            throw new Exception("User with this email already exists.");
        }

        var user = new UserModel
        {
            UserName = dto.UserName,
            UserSurName = dto.UserSurName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,

            // Temporary for now
            // We will add BCrypt later
            PasswordHash = dto.Password,

            RoleId = dto.RoleId,
            GroupId = dto.GroupId
        };

        var createdUser = await _userRepository
            .CreateAsync(user);

        return new UserResponseDto
        {
            UserId = createdUser.UserId,
            UserName = createdUser.UserName,
            UserSurName = createdUser.UserSurName,
            Email = createdUser.Email,
            PhoneNumber = createdUser.PhoneNumber,
            RoleId = createdUser.RoleId,
            GroupId = createdUser.GroupId
        };
    }

    public async Task<UserResponseDto?> UpdateAsync(
        int id,
        CreateUserDto dto
    )
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return null;
        }

        user.UserName = dto.UserName;
        user.UserSurName = dto.UserSurName;
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;
        user.RoleId = dto.RoleId;
        user.GroupId = dto.GroupId;

        var updatedUser = await _userRepository
            .UpdateAsync(user);

        return new UserResponseDto
        {
            UserId = updatedUser.UserId,
            UserName = updatedUser.UserName,
            UserSurName = updatedUser.UserSurName,
            Email = updatedUser.Email,
            PhoneNumber = updatedUser.PhoneNumber,
            RoleId = updatedUser.RoleId,
            GroupId = updatedUser.GroupId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }
}