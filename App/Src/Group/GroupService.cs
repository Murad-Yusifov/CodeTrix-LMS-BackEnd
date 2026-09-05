using AutoMapper;
using BackEndCodeTrix.Src.Group.GroupDTO;
using BackEndCodeTrix.Src.Users.UserDTO;

namespace BackEndCodeTrix.Src.Group;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public GroupService(
        IGroupRepository groupRepository,
        IMapper mapper
    )
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<List<GroupResponseDto>> GetAllAsync()
    {
        var groups = await _groupRepository.GetAllAsync();

        return _mapper.Map<List<GroupResponseDto>>(groups);
    }
    public async Task<GroupResponseDto?> GetByIdAsync(int id)
    {
        var group = await _groupRepository.GetByIdAsync(id);

        if (group is null)
        {
            return null;
        }

        return _mapper.Map<GroupResponseDto>(group);
    }

    public async Task<GroupResponseDto> CreateAsync(CreateGroupDto dto)
    {
        var mentorExists = await _groupRepository
            .MentorExistsAsync(dto.CreatedByMentorId);

        if (!mentorExists)
        {
            throw new Exception("The selected mentor does not exist.");
        }

        var group = _mapper.Map<GroupModel>(dto);

        group.DateOfCreated = DateTime.UtcNow;

        var createdGroup = await _groupRepository
            .CreateAsync(group);

        return _mapper.Map<GroupResponseDto>(createdGroup);
    }

    public async Task<GroupResponseDto?> UpdateAsync(
      int id,
      UpdateGroupDto dto)
    {
        var group = await _groupRepository.GetByIdAsync(id);

        if (group is null)
        {
            return null;
        }

        _mapper.Map(dto, group);

        var updatedGroup = await _groupRepository
            .UpdateAsync(group);

        return _mapper.Map<GroupResponseDto>(updatedGroup);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _groupRepository
            .DeleteAsync(id);
    }

    public async Task<List<UserResponseDto>> GetStudentsByGroupId(int groupId)
    {

var students = await _groupRepository.GetStudentsByGroupId(groupId);

return _mapper.Map<List<UserResponseDto>>(students);
        
    }
    
}