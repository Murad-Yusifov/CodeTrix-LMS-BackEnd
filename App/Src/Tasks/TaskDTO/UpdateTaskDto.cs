using System.ComponentModel.DataAnnotations;

namespace BackEndCodeTrix.Src.Tasks.TaskDTO;

public class UpdateTaskDto
{
    [Required]
    [MaxLength(200)]
    public string TaskName { get; set; } = string.Empty;

    [Required]
    public DateTime DeadLine { get; set; }

    public DateTime DateTime { get; set; }

    public string? MentorComment { get; set; }
}