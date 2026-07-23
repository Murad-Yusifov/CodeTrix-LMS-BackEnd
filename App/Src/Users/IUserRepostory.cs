namespace BackEndCodeTrix.Src.Users;

public interface IUserRepository
{
    Task<List<UserModel>> GetAllAsync();

    Task<UserModel?> GetByIdAsync(int id);

    Task<UserModel?> GetByEmailAsync(string email);

    Task<UserModel> CreateAsync(UserModel user);

    Task<UserModel> UpdateAsync(UserModel user);

    Task<bool> DeleteAsync(int id);
}