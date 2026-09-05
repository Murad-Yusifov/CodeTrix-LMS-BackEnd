namespace BackEndCodeTrix.Src.Lesson.LessonDTO;

public class LessonResponseDto
{
    public int LessonId { get; set; }

    public int GroupId { get; set; }

    public DateTime LessonStarts { get; set; }

    public DateTime LessonEnds { get; set; }

    public string Type { get; set; } = string.Empty;

    public string? ClassRoom { get; set; }
}