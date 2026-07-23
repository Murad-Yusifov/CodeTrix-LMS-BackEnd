using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Tasks;

namespace BackEndCodeTrix.Src.Users;

public class UserModel
{
    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string UserSurName { get; set; } = string.Empty;

    // Authentication
    public string Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    // Role
    public int RoleId { get; set; }

    public RoleModel Role { get; set; } = null!;

    // Student belongs to a group
    public int? GroupId { get; set; }

    public GroupModel? Group { get; set; }

    // Student's assigned tasks
    public ICollection<StudentTaskModel> StudentTasks { get; set; }
        = new List<StudentTaskModel>();

    // Groups created by this mentor
    public ICollection<GroupModel> CreatedGroups { get; set; }
        = new List<GroupModel>();
}