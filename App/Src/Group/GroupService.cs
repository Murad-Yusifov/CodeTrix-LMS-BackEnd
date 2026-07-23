using BackEndCodeTrix.Src.Group.GroupDTO;

namespace BackEndCodeTrix.Src.Group;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;

    public GroupService(
        IGroupRepository groupRepository
    )
    {
        _groupRepository = groupRepository;
    }

    public async Task<List<GroupResponseDto>> GetAllAsync()
    {
        var groups = await _groupRepository
            .GetAllAsync();

        return groups.Select(MapToDto).ToList();
    }

    public async Task<GroupResponseDto?> GetByIdAsync(
        int id
    )
    {
        var group = await _groupRepository
            .GetByIdAsync(id);

        if (group is null)
        {
            return null;
        }

        return MapToDto(group);
    }

    public async Task<GroupResponseDto> CreateAsync(
        CreateGroupDto dto
    )
    {
        var mentorExists = await _groupRepository
            .MentorExistsAsync(
                dto.CreatedByMentorId
            );

        if (!mentorExists)
        {
            throw new Exception(
                "The selected mentor does not exist."
            );
        }

        var group = new GroupModel
        {
            GroupName = dto.GroupName,

            CreatedByMentorId =
                dto.CreatedByMentorId,

            DateOfCreated = DateTime.UtcNow
        };

        var createdGroup = await _groupRepository
            .CreateAsync(group);

        return MapToDto(createdGroup);
    }

    public async Task<GroupResponseDto?> UpdateAsync(
        int id,
        UpdateGroupDto dto
    )
    {
        var group = await _groupRepository
            .GetByIdAsync(id);

        if (group is null)
        {
            return null;
        }

        group.GroupName = dto.GroupName;

        var updatedGroup = await _groupRepository
            .UpdateAsync(group);

        return MapToDto(updatedGroup);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _groupRepository
            .DeleteAsync(id);
    }

    private static GroupResponseDto MapToDto(
        GroupModel group
    )
    {
        return new GroupResponseDto
        {
            GroupId = group.GroupId,

            GroupName = group.GroupName,

            DateOfCreated =
                group.DateOfCreated,

            CreatedByMentorId =
                group.CreatedByMentorId,

            CreatedByMentorName =
                group.CreatedByMentor is not null
                    ? $"{group.CreatedByMentor.UserName} " +
                      $"{group.CreatedByMentor.UserSurName}"
                    : null,

            StudentsCount =
                group.Students.Count,

            TasksCount =
                group.Tasks.Count,

            LessonsCount =
                group.Lessons.Count
        };
    }
}