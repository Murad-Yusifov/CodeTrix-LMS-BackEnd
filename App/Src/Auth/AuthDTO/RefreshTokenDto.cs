using System.ComponentModel.DataAnnotations;

namespace BackEndCodeTrix.Src.Auth.AuthDTO;

public class RefreshTokenDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}