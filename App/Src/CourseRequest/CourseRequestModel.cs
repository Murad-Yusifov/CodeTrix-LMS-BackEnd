using System.ComponentModel.DataAnnotations;
using BackEndCodeTrix.Src.Course;

namespace BackEndCodeTrix.Src.CourseRequest;

public class CourseRequestModel
{

    [Key]
    public int CourseRequestId { get; set; }

    public int CourseId { get; set; }

    public CourseModel Course { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string? Message { get; set; }

    public CourseRequestStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

public enum CourseRequestStatus
{
    Pending = 1,
    Contacted = 2,
    Approved = 3,
    Rejected = 4
}