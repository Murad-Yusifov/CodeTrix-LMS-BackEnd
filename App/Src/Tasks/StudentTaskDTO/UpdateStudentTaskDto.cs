using System.ComponentModel.DataAnnotations;

namespace BackEndCodeTrix.Src.Tasks.StudentTaskDTO;

public class UpdateStudentTaskDto
{
    [Required]
    public StudentTaskStatus Status { get; set; } = StudentTaskStatus.Pending;

    public string? StudentComment { get; set; }
}