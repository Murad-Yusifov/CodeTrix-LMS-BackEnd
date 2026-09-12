namespace BackEndCodeTrix.Src.CourseRequest.CourseRequestDTO;

public class CourseRequestResponseDto
{
    public int CourseRequestId { get; set; }

    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string? Message { get; set; }

    public CourseRequestStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}