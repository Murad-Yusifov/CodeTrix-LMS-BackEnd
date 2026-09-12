namespace BackEndCodeTrix.Src.Group.GroupDTO;

public class CreateGroupDto
{
    public string GroupName { get; set; } = string.Empty;
    public int CourseId { get; set; } 

    public int CreatedByMentorId { get; set; }
}