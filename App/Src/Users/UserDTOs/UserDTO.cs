namespace BackEndCodeTrix.Src.Users.UserDTO;

public class UserResponseDto
{
    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string UserSurName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public int RoleId { get; set; }

    public int? GroupId { get; set; }
}