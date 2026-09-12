namespace BackEndCodeTrix.Src.Auth.AuthDTO;

public class AuthResponseDto
{
    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string UserSurName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}