using AutoMapper;
using BackEndCodeTrix.Src.Users;
using BackEndCodeTrix.Src.Users.UserDTO;

namespace BackEndCodeTrix.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<UserModel, UserResponseDto>();

        CreateMap<CreateUserDto, UserModel>()
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.Password)
            );

        // Later:
        // CreateMap<GroupModel, GroupResponseDto>();
        // CreateMap<CreateGroupDto, GroupModel>();
        // CreateMap<TaskModel, TaskResponseDto>();
        // CreateMap<CreateTaskDto, TaskModel>();
    }
}