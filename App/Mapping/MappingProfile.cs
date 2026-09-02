using AutoMapper;
using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.Group.GroupDTO;
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
        // User mappings
        CreateMap<UserModel, UserResponseDto>()
        .ForMember(
            dest => dest.GroupId,
            opt => opt.MapFrom(src => src.GroupId)
        );

        CreateMap<CreateUserDto, UserModel>()
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.Password)
            );

        CreateMap<GroupModel, GroupResponseDto>()
        .ForMember(destination => destination.CreatedByMentorName,
        options => options.MapFrom(
                    source => source.CreatedByMentor != null
                        ? $"{source.CreatedByMentor.UserName} {source.CreatedByMentor.UserSurName}"
                        : null
                )
                )
                   .ForMember(
                destination => destination.StudentsCount,
                options => options.MapFrom(
                    source => source.Students.Count
                ));
        CreateMap<CreateGroupDto, GroupModel>();

        CreateMap<CreateStudentTaskDto, StudentTaskModel>();

        CreateMap<StudentTaskModel, StudentTaskResponseDto>()
     .ForMember(
         dest => dest.TaskName,
         opt => opt.MapFrom(
             src => src.Task != null
                 ? src.Task.TaskName
                 : null
         )
     )
      .ForMember(
    dest => dest.TaskDateTime,
    opt => opt.MapFrom(src => src.Task.DateTime)
)
             // .ForMember(
             //     dest => dest.DeadLine,
             //     opt => opt.MapFrom(
             //         src => src.Task!.DeadLine
             //     )
             // )
             .ForMember(
                 dest => dest.MentorComment,
                 opt => opt.MapFrom(
                     src => src.Task!.MentorComment
                 )
             //  )
             //  .ForMember(
             //      dest => dest.StudentComment,
             //      opt => opt.MapFrom(
             //          src => src.Task
             //      )
             );

        CreateMap<TaskModel, TaskResponseDto>()
         .ForMember(
            dest => dest.GroupId,
            opt => opt.MapFrom(src => src.GroupId)
        );
        CreateMap<CreateTaskDto, TaskModel>();
        CreateMap<UpdateTaskDto, TaskModel>();

    }
}