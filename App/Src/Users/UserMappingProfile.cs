using AutoMapper;
using BackEndCodeTrix.Src.Users.UserDTO;

namespace BackEndCodeTrix.Src.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // Entity → Response DTO
        CreateMap<UserModel, UserResponseDto>();

        // Create DTO → Entity
        CreateMap<CreateUserDto, UserModel>()
            .ForMember(
                dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.Password)
            );
    }
}