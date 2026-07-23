namespace BackEndCodeTrix.Src.Group.GroupDTO;

public class GroupResponseDto
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = string.Empty;

    public DateTime DateOfCreated { get; set; }

    public int CreatedByMentorId { get; set; }

    public string? CreatedByMentorName { get; set; }

    public int StudentsCount { get; set; }

    public int TasksCount { get; set; }

    public int LessonsCount { get; set; }
}