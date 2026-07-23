namespace BackEndCodeTrix.Src.Users;

public class RoleModel
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;

    public ICollection<UserModel> Users { get; set; }
        = new List<UserModel>();
}