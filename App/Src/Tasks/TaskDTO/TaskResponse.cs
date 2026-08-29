namespace BackEndCodeTrix.Src.Tasks.TaskDTO;

public class TaskResponseDto
{
    public int TaskId { get; set; }

    public string TaskName { get; set; } = string.Empty;

    public DateTime DeadLine { get; set; }

    public DateTime DateTime { get; set; }

    public string TaskStatus { get; set; } = string.Empty;

    public string? MentorComment { get; set; }

    public int GroupId { get; set; }
}