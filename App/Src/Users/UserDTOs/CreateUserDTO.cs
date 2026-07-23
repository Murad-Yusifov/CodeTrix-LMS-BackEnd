namespace BackEndCodeTrix.Src.Users.UserDTO;

public class CreateUserDto
{
    public string UserName { get; set; } = string.Empty;

    public string UserSurName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string Password { get; set; } = string.Empty;

    public int RoleId { get; set; }

    public int? GroupId { get; set; }
}