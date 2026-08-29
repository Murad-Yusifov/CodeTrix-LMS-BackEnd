using System.ComponentModel.DataAnnotations;

namespace BackEndCodeTrix.Src.Tasks.StudentTaskDTO;

public class UpdateStudentTaskDto
{
    [Required]
    public string Status { get; set; } = "Pending";

    public string? StudentComment { get; set; }
}