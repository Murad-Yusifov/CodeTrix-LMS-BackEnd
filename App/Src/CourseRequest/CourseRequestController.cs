using BackEndCodeTrix.Src.CourseRequest.CourseRequestDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCodeTrix.Src.CourseRequest;

[ApiController]
[Route("api/course-requests")]
public class CourseRequestController : ControllerBase
{
    private readonly ICourseRequestService _service;

    public CourseRequestController(
        ICourseRequestService service)
    {
        _service = service;
    }

    // Main website
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(
        CreateCourseRequestDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.CourseRequestId },
            result);
    }

    // Admin / Manager
    [HttpGet]
    // [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    // Admin / Manager
    [HttpGet("{id}")]
    // [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }

    // Admin / Manager
    [HttpPut("{id}")]
    // [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(
        int id,
        UpdateCourseRequestDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);

        return Ok(result);
    }

    // Admin / Manager
    [HttpDelete("{id}")]
    // [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }

    // Admin / Manager
    [HttpPatch("{id}/status")]
    // [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> ChangeStatus(
        int id,
        ChangeCourseRequestStatusDto dto)
    {
        var result =
            await _service.ChangeStatusAsync(id, dto);

        return Ok(result);
    }
}