// using BackEndCodeTrix.Src.Group;

// namespace BackEndCodeTrix.Src.Tasks;

// public class TaskModel
// {
//     public int TaskId { get; set; }

//     public string TaskName { get; set; } = string.Empty;

//     public DateTime Deadline { get; set; }
//     public string? TaskLink { get; set; }
//     public string? Description { get; set; }

//     public DateTime CreatedAt { get; set; }

//     // The group that receives this task
//     public int GroupId { get; set; }

//     public GroupModel Group { get; set; } = null!;

//     // Individual student task statuses
//     public ICollection<StudentTaskModel> StudentTasks { get; set; }
//         = new List<StudentTaskModel>();
// }



using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEndCodeTrix.Src.Group;

namespace BackEndCodeTrix.Src.Tasks;

public class TaskModel
{
    [Key]
    public int TaskId { get; set; }

    [Required]
    [MaxLength(200)]
    public string TaskName { get; set; } = string.Empty;

    public DateTime DeadLine { get; set; }

    public DateTime DateTime { get; set; }

    [MaxLength(50)]
    public string TaskStatus { get; set; } = "NotAssigned";

    public string? MentorComment { get; set; }

    public int GroupId { get; set; }

    [ForeignKey(nameof(GroupId))]
    public GroupModel? Group { get; set; }

    public ICollection<StudentTaskModel> StudentTasks { get; set; }
        = new List<StudentTaskModel>();
}