using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEndCodeTrix.Src.Users;

namespace BackEndCodeTrix.Src.Tasks;

public class    StudentTaskModel
{
    [Key]
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

    [ForeignKey(nameof(TaskId))]
    public TaskModel Task { get; set; } = null!;
}