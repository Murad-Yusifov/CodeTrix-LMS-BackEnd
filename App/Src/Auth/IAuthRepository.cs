using BackEndCodeTrix.Src.Users;

namespace BackEndCodeTrix.Src.Auth;

public interface IAuthRepository
{
    Task<UserModel?> GetUserByEmailAsync(string email);
    Task<UserModel?> GetUserByIdAsync(int userId);

    Task<bool> EmailExistsAsync(string email);

    Task AddUserAsync(UserModel user);

    Task<RefreshTokenModel?> GetRefreshTokenAsync(string token);

    Task AddRefreshTokenAsync(RefreshTokenModel refreshToken);

    Task UpdateRefreshTokenAsync(RefreshTokenModel refreshToken);

    Task SaveChangesAsync();
}