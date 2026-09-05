using BackEndCodeTrix.Src.Group.GroupDTO;
using BackEndCodeTrix.Src.Users.UserDTO;

namespace BackEndCodeTrix.Src.Group;

public interface IGroupService
{
    Task<List<GroupResponseDto>> GetAllAsync();

    Task<GroupResponseDto?> GetByIdAsync(int id);

    Task<GroupResponseDto> CreateAsync(
        CreateGroupDto dto
    );

    Task<GroupResponseDto?> UpdateAsync(
        int id,
        UpdateGroupDto dto
    );

    Task<bool> DeleteAsync(int id);

        Task<List<UserResponseDto>> GetStudentsByGroupId(int groupId);

}