using BackEndCodeTrix.Src.Group;

namespace BackEndCodeTrix.Src.LessonSchedule;

public class LessonModel
{
    public int LessonId { get; set; }

    public int GroupId { get; set; }
    public string LessonName { get; set; } = string.Empty;
    public GroupModel Group { get; set; } = null!;

    public DateTime LessonStarts { get; set; }

    public DateTime LessonEnds { get; set; }

    public LocationType LocationType { get; set; }

    // Zoom, Teams, Google Meet link
    // OR classroom number
    public string? Classroom { get; set; } = string.Empty;


}