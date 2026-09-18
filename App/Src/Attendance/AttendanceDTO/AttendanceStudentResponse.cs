namespace BackEndCodeTrix.Src.LessonSchedule;
public class AttendanceStudentResponseDto
{
    public int AttendanceStudentId { get; set; }

    public int StudentId { get; set; }

    public string? StudentName { get; set; }

    public string? StudentSurname { get; set; }

    public string Status { get; set; } = "Absent";
}