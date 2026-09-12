using BackEndCodeTrix.Src.Users;

namespace BackEndCodeTrix.Src.Auth;

public class RefreshTokenModel
{
    public int RefreshTokenId { get; set; }

    public string Token { get; set; } = string.Empty;

    public int UserId { get; set; }

    public UserModel User { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }
}