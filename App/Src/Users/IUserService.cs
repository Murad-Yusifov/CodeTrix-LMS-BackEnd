

using BackEndCodeTrix.Src.Users.UserDTO;

namespace BackEndCodeTrix.Src.Users;

public interface IUserService
{
    Task<List<UserResponseDto>> GetAllAsync();

    Task<UserResponseDto?> GetByIdAsync(int id);

    Task<UserResponseDto> CreateAsync(CreateUserDto dto);

    Task<UserResponseDto?> UpdateAsync(
        int id,
        CreateUserDto dto
    );

    Task<bool> DeleteAsync(int id);
}