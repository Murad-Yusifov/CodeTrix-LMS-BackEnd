using System.ComponentModel.DataAnnotations;
using BackEndCodeTrix.Src.Users;

namespace BackEndCodeTrix.Src.LessonSchedule;

public class AttendanceStudentModel
{
    [Key]
    public int AttendanceStudentId { get; set; }

    public int AttendanceId { get; set; }
    public AttendanceModel Attendance { get; set; } = null!;

    public int StudentId { get; set; }
    public UserModel Student { get; set; } = null!;

    public bool Status { get; set; }
}