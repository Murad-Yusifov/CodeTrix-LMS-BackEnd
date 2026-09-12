namespace BackEndCodeTrix.Src.Course.CourseDTO;

public class CourseResponseDto
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}