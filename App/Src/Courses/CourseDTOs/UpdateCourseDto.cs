namespace BackEndCodeTrix.Src.Course.CourseDTO;

public class UpdateCourseDto
{
    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Description { get; set; } = null!;
}