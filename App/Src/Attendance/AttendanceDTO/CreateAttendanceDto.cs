namespace BackEndCodeTrix.Src.LessonSchedule.AttendanceDTO;

public class CreateAttendanceDto
{
    public int StudentId { get; set; }
    public int LessonId { get; set; }
    public AttendanceStatus Status { get; set; }
}