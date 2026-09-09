using BackEndCodeTrix.Src.LessonSchedule;
using BackEndCodeTrix.Src.Tasks;
using BackEndCodeTrix.Src.Users;

namespace BackEndCodeTrix.Src.Group;

public class   GroupModel
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = string.Empty;


    // public CourseModel CourseId {get; set;} 

    // public CourseModel Course {get; set;}  =null!;

    public DateTime DateOfCreated { get; set; }

    // Mentor who created the group
    public int CreatedByMentorId { get; set; }

    // Navigation property
    public UserModel CreatedByMentor { get; set; } = null!;

    // Students belonging to this group
    public ICollection<UserModel> Students { get; set; }
        = new List<UserModel>();

    // Tasks assigned to this group
    public ICollection<TaskModel> Tasks { get; set; }
        = new List<TaskModel>();

    // Lessons scheduled for this group
    public ICollection<LessonModel> Lessons { get; set; }
        = new List<LessonModel>();
}