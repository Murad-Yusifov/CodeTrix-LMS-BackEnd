using BackEndCodeTrix.Src.LessonSchedule;
using BackEndCodeTrix.Src.Users;

public class AttendanceModel
{
    public int AttendanceId { get; set; }

    public int StudentId { get; set; }
    public UserModel Student { get; set; } = null!;

    public int LessonId { get; set; }
    public LessonModel LessonName { get; set; } = null!;

    public AttendanceStatus Status { get; set; }

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
}