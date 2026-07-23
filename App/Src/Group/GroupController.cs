using Microsoft.AspNetCore.Mvc;
using BackEndCodeTrix.Src.Group.GroupDTO;

namespace BackEndCodeTrix.Src.Group;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupController(
        IGroupService groupService
    )
    {
        _groupService = groupService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGroups()
    {
        var groups = await _groupService
            .GetAllAsync();

        return Ok(groups);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGroupById(
        int id
    )
    {
        var group = await _groupService
            .GetByIdAsync(id);

        if (group is null)
        {
            return NotFound(
                "Group not found."
            );
        }

        return Ok(group);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(
        CreateGroupDto dto
    )
    {
        try
        {
            var group = await _groupService
                .CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetGroupById),

                new
                {
                    id = group.GroupId
                },

                group
            );
        }
        catch (Exception exception)
        {
            return BadRequest(
                exception.Message
            );
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGroup(
        int id,
        UpdateGroupDto dto
    )
    {
        var group = await _groupService
            .UpdateAsync(id, dto);

        if (group is null)
        {
            return NotFound(
                "Group not found."
            );
        }

        return Ok(group);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(
        int id
    )
    {
        var deleted = await _groupService
            .DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(
                "Group not found."
            );
        }

        return NoContent();
    }
}