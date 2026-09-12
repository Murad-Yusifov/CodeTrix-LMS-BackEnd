using BackEndCodeTrix.Src.Auth.AuthDTO;

namespace BackEndCodeTrix.Src.Auth;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto dto);

    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);

    Task<CurrentUserDto> GetMeAsync(int userId);

    Task<AuthResponseDto> RefreshAsync(
        RefreshTokenDto dto);

    Task LogoutAsync(int userId);
}