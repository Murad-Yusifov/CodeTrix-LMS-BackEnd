namespace BackEndCodeTrix.Src.Auth.AuthDTO;

public class CurrentUserDto
{
    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string UserSurName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public int? GroupId { get; set; }
}