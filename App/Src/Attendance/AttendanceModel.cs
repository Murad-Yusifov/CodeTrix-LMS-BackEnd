// using System.ComponentModel.DataAnnotations;
// using BackEndCodeTrix.Src.Users;

// namespace BackEndCodeTrix.Src.LessonSchedule;
// public class AttendanceModel
// {
//     [Key]
//     public int AttendanceId { get; set; }

//     public int StudentId { get; set; }
//     public UserModel Student { get; set; } = null!;

//     public int LessonId { get; set; }
//     public LessonModel Lesson { get; set; } = null!;

//     public AttendanceStatus Status { get; set; }

//     public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
// }

using System.ComponentModel.DataAnnotations;
using BackEndCodeTrix.Src.Group;
namespace BackEndCodeTrix.Src.LessonSchedule;



public class AttendanceModel
{
    [Key]
    public int AttendanceId { get; set; }

    public int LessonId { get; set; }
    public LessonModel Lesson { get; set; } = null!;

    public int GroupId { get; set; }
    public GroupModel Group { get; set; } = null!;

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public ICollection<AttendanceStudentModel> Students { get; set; }
        = new List<AttendanceStudentModel>();
}