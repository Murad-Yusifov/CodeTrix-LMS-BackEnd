using BackEndCodeTrix.Src.Users;

namespace BackEndCodeTrix.Src.Tasks;

public class StudentTaskModel
{
    public int StudentTaskId { get; set; }

    public int StudentId { get; set; }

    public int TaskId { get; set; }
    public string? SubmissionLink { get; set; }

    public StudentTaskStatus Status { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? MentorComment { get; set; }

    // Navigation properties
    public UserModel Student { get; set; } = null!;

    public TaskModel Task { get; set; } = null!;
}