using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndCodeTrix.Src.Users;

public class RoleModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;

    public ICollection<UserModel> Users { get; set; }
        = new List<UserModel>();
}