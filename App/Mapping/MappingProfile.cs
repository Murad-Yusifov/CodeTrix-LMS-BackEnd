using AutoMapper;
using BackEndCodeTrix.Src.Auth.AuthDTO;
using BackEndCodeTrix.Src.Course;
using BackEndCodeTrix.Src.Course.CourseDTO;
using BackEndCodeTrix.Src.CourseRequest;
using BackEndCodeTrix.Src.CourseRequest.CourseRequestDTO;
using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Group.GroupDTO;
using BackEndCodeTrix.Src.Lesson.LessonDTO;
using BackEndCodeTrix.Src.LessonSchedule;
using BackEndCodeTrix.Src.LessonSchedule.AttendanceDTO;
using BackEndCodeTrix.Src.Tasks;
using BackEndCodeTrix.Src.Tasks.StudentTaskDTO;
using BackEndCodeTrix.Src.Tasks.TaskDTO;
using BackEndCodeTrix.Src.Users;
using BackEndCodeTrix.Src.Users.UserDTO;

namespace BackEndCodeTrix.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // =========================
        // User
        // =========================

        CreateMap<UserModel, UserResponseDto>();

        CreateMap<CreateUserDto, UserModel>()
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.Password)
            );

        CreateMap<UpdateUserDto, UserModel>();

        CreateMap<UserModel, CurrentUserDto>()
            .ForMember(
                dest => dest.Role,
                opt => opt.MapFrom(src => src.Role.RoleName)
            );

        CreateMap<UserModel, AuthResponseDto>()
            .ForMember(
                dest => dest.Role,
                opt => opt.MapFrom(src => src.Role.RoleName)
            );


        // =========================
        // Group
        // =========================

        CreateMap<GroupModel, GroupResponseDto>()
            .ForMember(
                dest => dest.CreatedByMentorName,
                opt => opt.MapFrom(src =>
                    src.CreatedByMentor != null
                        ? $"{src.CreatedByMentor.UserName} {src.CreatedByMentor.UserSurName}"
                        : null
                )
            )
            .ForMember(
                dest => dest.StudentsCount,
                opt => opt.MapFrom(src => src.Students.Count)
            )
            .ForMember(
                dest => dest.CourseName,
                opt => opt.MapFrom(src => src.Course.Title)
            );

        CreateMap<CreateGroupDto, GroupModel>();
        
        CreateMap<UpdateGroupDto, GroupModel>();


        // =========================
        // Student Task
        // =========================

        CreateMap<CreateStudentTaskDto, StudentTaskModel>();

        CreateMap<StudentTaskModel, StudentTaskResponseDto>()
            .ForMember(
                dest => dest.TaskName,
                opt => opt.MapFrom(src =>
                    src.Task != null
                        ? src.Task.TaskName
                        : null
                )
            )
            .ForMember(
                dest => dest.TaskDateTime,
                opt => opt.MapFrom(src => src.Task.DateTime)
            )
            .ForMember(
                dest => dest.MentorComment,
                opt => opt.MapFrom(src => src.Task!.MentorComment)
            );


        // =========================
        // Task
        // =========================

        CreateMap<TaskModel, TaskResponseDto>()
            .ForMember(
                dest => dest.GroupId,
                opt => opt.MapFrom(src => src.GroupId)
            );

        CreateMap<CreateTaskDto, TaskModel>();

        CreateMap<UpdateTaskDto, TaskModel>();


        // =========================
        // Lesson
        // =========================

        CreateMap<CreateLessonDto, LessonModel>()
            .ForMember(
                dest => dest.LocationType,
                opt => opt.MapFrom(src =>
                    Enum.Parse<LocationType>(src.Type, true)
                )
            )
            .ForMember(
                dest => dest.Classroom,
                opt => opt.MapFrom(src => src.ClassRoom)
            );

        CreateMap<UpdateLessonDto, LessonModel>();

        CreateMap<LessonModel, LessonResponseDto>()
            .ForMember(
                dest => dest.Type,
                opt => opt.MapFrom(src =>
                    src.LocationType.ToString()
                )
            )
            .ForMember(
                dest => dest.ClassRoom,
                opt => opt.MapFrom(src => src.Classroom)
            );


        // =========================
        // Attendance
        // =========================

        CreateMap<AttendanceModel, AttendanceResponseDto>()
            .ForMember(
                dest => dest.LessonName,
                opt => opt.MapFrom(src =>
                    src.Lesson.LessonName
                )
            )
            .ForMember(
                dest => dest.Students,
                opt => opt.MapFrom(src =>
                    src.Students
                )
            );

        CreateMap<CreateAttendanceDto, AttendanceModel>();

        CreateMap<UpdateAttendanceDto, AttendanceModel>();

        CreateMap<AttendanceStudentModel, AttendanceStudentResponseDto>()
            .ForMember(
                dest => dest.StudentName,
                opt => opt.MapFrom(src =>
                    src.Student.UserName
                )
            )
            .ForMember(
                dest => dest.StudentSurname,
                opt => opt.MapFrom(src =>
                    src.Student.UserSurName
                )
            )
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src =>
                    src.Status
                )
            );


        // =========================
        // Course
        // =========================

        CreateMap<CourseModel, CourseResponseDto>();

        CreateMap<CreateCourseDto, CourseModel>();

        CreateMap<UpdateCourseDto, CourseModel>();


        // =========================
        // Course Request
        // =========================

        CreateMap<CreateCourseRequestDto, CourseRequestModel>();

        CreateMap<UpdateCourseRequestDto, CourseRequestModel>();

        CreateMap<CourseRequestModel, CourseRequestResponseDto>()
            .ForMember(
                dest => dest.CourseName,
                opt => opt.MapFrom(src =>
                    src.Course.Title
                )
            );
    }
}