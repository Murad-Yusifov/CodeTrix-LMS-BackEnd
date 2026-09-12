using System.ComponentModel.DataAnnotations;

namespace BackEndCodeTrix.Src.CourseRequest.CourseRequestDTO;

public class ChangeCourseRequestStatusDto
{
    [Required]
    public CourseRequestStatus Status { get; set; }
}