namespace BackEndCodeTrix.Src.Group;

public interface IGroupRepository
{
    Task<List<GroupModel>> GetAllAsync();

    Task<GroupModel?> GetByIdAsync(int id);

    Task<GroupModel> CreateAsync(GroupModel group);

    Task<GroupModel> UpdateAsync(GroupModel group);

    Task<bool> DeleteAsync(int id);

    Task<bool> MentorExistsAsync(int mentorId);
}