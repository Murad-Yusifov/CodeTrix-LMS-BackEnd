using System.ComponentModel.DataAnnotations;

namespace BackEndCodeTrix.Src.CourseRequest.CourseRequestDTO;

public class UpdateCourseRequestDto
{
    [Required]
    public int CourseId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string Phone { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Message { get; set; }
}