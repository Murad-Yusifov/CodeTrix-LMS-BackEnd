namespace BackEndCodeTrix.Src.Tasks.StudentTaskDTO;

public class StudentTaskResponseDto
{
    public int StudentTaskId { get; set; }

    public int StudentId { get; set; }

    public int TaskId { get; set; }
    public string? TaskName { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime? CompletedAt { get; set; }

    // public string? StudentComment { get; set; }

    public string? MentorComment { get; set; }
}