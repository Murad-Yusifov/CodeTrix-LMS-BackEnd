using BackEndCodeTrix.Src.Group.GroupDTO;

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
}