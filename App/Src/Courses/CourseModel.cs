using BackEndCodeTrix.Src.Group;

namespace BackEndCodeTrix.Src.Course;

public class CourseModel
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<GroupModel> Groups { get; set; }
        = new List<GroupModel>();
}