using Microsoft.AspNetCore.Mvc;
using BackEndCodeTrix.Src.Users.UserDTO;

namespace BackEndCodeTrix.Src.Users;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
        {
            return NotFound("User not found.");
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        CreateUserDto dto
    )
    {
        var user = await _userService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetUserById),
            new { id = user.UserId },
            user
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(
        int id,
        CreateUserDto dto
    )
    {
        var user = await _userService.UpdateAsync(id, dto);

        if (user is null)
        {
            return NotFound("User not found.");
        }

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _userService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound("User not found.");
        }

        return NoContent();
    }
}