using BackEndCodeTrix.Src.Group;

namespace BackEndCodeTrix.Src.Tasks;

public class TaskModel
{
    public int TaskId { get; set; }

    public string TaskName { get; set; } = string.Empty;

    public DateTime Deadline { get; set; }
    public string? TaskLink { get; set; }
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    // The group that receives this task
    public int GroupId { get; set; }

    public GroupModel Group { get; set; } = null!;

    // Individual student task statuses
    public ICollection<StudentTaskModel> StudentTasks { get; set; }
        = new List<StudentTaskModel>();
}