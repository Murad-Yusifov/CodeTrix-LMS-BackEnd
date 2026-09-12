using BackEndCodeTrix.Src.Course.CourseDTO;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCodeTrix.Src.Course;

[ApiController]
[Route("api/courses")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _service;

    public CourseController(ICourseService service)
    {
        _service = service;
    }

    // GET /api/courses
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _service.GetAllAsync();

        return Ok(courses);
    }

    // GET /api/courses/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _service.GetByIdAsync(id);

        if (course == null)
            return NotFound();

        return Ok(course);
    }

    // GET /api/courses/slug/{slug}
    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var course = await _service.GetBySlugAsync(slug);

        if (course == null)
            return NotFound();

        return Ok(course);
    }

    // POST /api/courses
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCourseDto dto)
    {
        try
        {
            var course = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = course.CourseId },
                course);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    // PUT /api/courses/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateCourseDto dto)
    {
        try
        {
            var course = await _service.UpdateAsync(id, dto);

            if (course == null)
                return NotFound();

            return Ok(course);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    // DELETE /api/courses/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    // PATCH /api/courses/{id}/status
    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        UpdateCourseStatusDto dto)
    {
        var course = await _service.UpdateStatusAsync(id, dto);

        if (course == null)
            return NotFound();

        return Ok(course);
    }
}