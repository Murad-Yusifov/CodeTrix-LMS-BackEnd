namespace BackEndCodeTrix.Src.Attendance.AttendanceDTO;

public class AttendanceResponseDto
{
    public int AttendanceId { get; set; }

    // Student
    public int StudentId { get; set; }
    public string StudentName { get; set; } = null!;
    public string StudentSurname { get; set; } = null!;

    // Lesson
    public int LessonId { get; set; }
    public string LessonName { get; set; } = null!;

    // Attendance
    public AttendanceStatus Status { get; set; }

    public DateTime RecordedAt { get; set; }
}