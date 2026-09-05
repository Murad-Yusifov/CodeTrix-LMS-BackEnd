namespace BackEndCodeTrix.Src.Users.UserDTO;

public class UpdateUserDto
{
    public string UserName { get; set; } = string.Empty;

    public string UserSurName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public int? GroupId { get; set; }

    public int RoleId { get; set; }
}