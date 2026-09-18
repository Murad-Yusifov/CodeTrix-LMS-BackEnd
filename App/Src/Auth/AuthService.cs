
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BackEndCodeTrix.Src.Auth.AuthDTO;
using BackEndCodeTrix.Src.Users;
using Microsoft.IdentityModel.Tokens;

namespace BackEndCodeTrix.Src.Auth;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;
    private readonly PasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthService(
        IAuthRepository repository,
        PasswordHasher passwordHasher,
        IConfiguration configuration)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> LoginAsync(
        LoginDto dto)
    {
        var user =
            await _repository.GetUserByEmailAsync(dto.Email);

        if (user == null ||
            !_passwordHasher.Verify(
                dto.Password,
                user.PasswordHash))
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        return await CreateAuthResponseAsync(user);
    }

    public async Task<AuthResponseDto> RegisterAsync(
        RegisterDto dto)
    {
        if (await _repository.EmailExistsAsync(dto.Email))
        {
            throw new InvalidOperationException(
                "Email is already registered.");
        }

        var user = new UserModel
        {
            UserName = dto.UserName,
            UserSurName = dto.UserSurName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            PasswordHash = _passwordHasher.Hash(dto.Password),
            RoleId = dto.RoleId,
            GroupId = null
        };

        await _repository.AddUserAsync(user);
        await _repository.SaveChangesAsync();

        var createdUser =
            await _repository.GetUserByIdAsync(user.UserId);

        if (createdUser == null)
        {
            throw new InvalidOperationException(
                "User was not created.");
        }

        return await CreateAuthResponseAsync(createdUser);
    }

    public async Task<CurrentUserDto> GetMeAsync(
        int userId)
    {
        var user =
            await _repository.GetUserByIdAsync(userId);

        if (user == null)
        {
            throw new KeyNotFoundException(
                "User not found.");
        }

        return new CurrentUserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            UserSurName = user.UserSurName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            Role = user.Role?.RoleName ?? string.Empty,
            GroupId = user.GroupId
        };
    }

    public async Task<AuthResponseDto> RefreshAsync(
        RefreshTokenDto dto)
    {
        var storedToken =
            await _repository.GetRefreshTokenAsync(
                dto.RefreshToken);

        if (storedToken == null)
        {
            throw new UnauthorizedAccessException(
                "Invalid refresh token.");
        }

        if (storedToken.RevokedAt != null)
        {
            throw new UnauthorizedAccessException(
                "Refresh token has been revoked.");
        }

        if (storedToken.ExpiresAt <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException(
                "Refresh token has expired.");
        }

        if (storedToken.User == null)
        {
            throw new UnauthorizedAccessException(
                "User associated with refresh token was not found.");
        }

        // Revoke old refresh token
        storedToken.RevokedAt = DateTime.UtcNow;

        await _repository.UpdateRefreshTokenAsync(
            storedToken);

        // Generate new tokens
        return await CreateAuthResponseAsync(
            storedToken.User);
    }

    public async Task LogoutAsync(int userId)
    {
        // If you want logout to revoke the exact
        // refresh token, pass RefreshTokenDto here.
        //
        // For now this can be implemented by
        // revoking the user's active refresh tokens.

        await Task.CompletedTask;
    }

    private async Task<AuthResponseDto>
        CreateAuthResponseAsync(UserModel user)
    {
        var accessToken =
            GenerateAccessToken(user);

        var refreshToken = new RefreshTokenModel
        {
            Token = GenerateRefreshToken(),

            UserId = user.UserId,

            CreatedAt = DateTime.UtcNow,

            ExpiresAt =
                DateTime.UtcNow.AddDays(7)
        };

        await _repository.AddRefreshTokenAsync(
            refreshToken);

        return new AuthResponseDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            UserSurName = user.UserSurName,
            Email = user.Email,
            Role = user.Role?.RoleName ?? "Student",

            AccessToken = accessToken,

            RefreshToken = refreshToken.Token
        };
    }

    private string GenerateAccessToken(
        UserModel user)
    {
        
        var key =
            _configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new InvalidOperationException(
                "Jwt:Key is not configured.");
        }

        var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.UserId.ToString()),

            new Claim(
                ClaimTypes.Email,
                user.Email),

            new Claim(
                ClaimTypes.Name,
                user.UserName),

            new Claim(
                ClaimTypes.Role,
                user.Role?.RoleName ?? "Student")
        };

        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

        var credentials =
            new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));
    }
}


