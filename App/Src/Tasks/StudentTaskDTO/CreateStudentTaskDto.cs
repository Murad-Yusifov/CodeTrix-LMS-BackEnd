using System.ComponentModel.DataAnnotations;

namespace BackEndCodeTrix.Src.Tasks.StudentTaskDTO;

public class CreateStudentTaskDto
{
    [Required]
    public int StudentId { get; set; }

    [Required]
    public int TaskId { get; set; }
}