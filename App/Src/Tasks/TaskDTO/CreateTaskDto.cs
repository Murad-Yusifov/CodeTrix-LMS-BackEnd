using System.ComponentModel.DataAnnotations;

namespace BackEndCodeTrix.Src.Tasks.TaskDTO;

public class CreateTaskDto
{
    [Required]
    [MaxLength(200)]
    public string TaskName { get; set; } = string.Empty;

    [Required]
    public DateTime DeadLine { get; set; }

    public DateTime DateTime { get; set; }

    [Required]
    public int GroupId { get; set; }

    public string? MentorComment { get; set; }
}