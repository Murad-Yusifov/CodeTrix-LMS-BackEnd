using BackEndCodeTrix.Src.LessonSchedule.AttendanceDTO;
using Microsoft.AspNetCore.Mvc;

namespace BackEndCodeTrix.Src.LessonSchedule;

[ApiController]
[Route("api/attendance")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(
        IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

     [HttpGet]
    public async Task<IActionResult> GetAllAttendancesAsync()
    {
        var attendance =
            await _attendanceService.GetAllAsync();

        if (attendance is null)
        {
            return NotFound(
                new { message = "Attendance not found." });
        }

        return Ok(attendance);
    }

    // GET /api/attendance/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var attendance =
            await _attendanceService.GetByIdAsync(id);

        if (attendance is null)
        {
            return NotFound(
                new { message = "Attendance not found." });
        }

        return Ok(attendance);
    }

    // POST /api/attendance
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAttendanceDto dto)
    {
        var attendance =
            await _attendanceService.CreateAsync(dto);

        if (attendance is null)
        {
            return BadRequest(
                new
                {
                    message =
                        "Invalid student/lesson or attendance already exists."
                });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = attendance.AttendanceId },
            attendance);
    }

    // PUT /api/attendance/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateAttendanceDto dto)
    {
        var attendance =
            await _attendanceService.UpdateAsync(id, dto);

        if (attendance is null)
        {
            return NotFound(
                new { message = "Attendance not found." });
        }

        return Ok(attendance);
    }

    // DELETE /api/attendance/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _attendanceService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(
                new { message = "Attendance not found." });
        }

        return NoContent();
    }

    // GET /api/users/{userId}/attendance
    [HttpGet("/api/users/{userId:int}/attendance")]
    public async Task<IActionResult> GetByStudent(int userId)
    {
        var attendances =
            await _attendanceService
                .GetByStudentIdAsync(userId);

        if (attendances is null)
        {
            return NotFound(
                new { message = "Student not found." });
        }

        return Ok(attendances);
    }

    // GET /api/lessons/{lessonId}/attendance
    [HttpGet("/api/lessons/{lessonId:int}/attendance")]
    public async Task<IActionResult> GetByLesson(int lessonId)
    {
        var attendances =
            await _attendanceService
                .GetByLessonIdAsync(lessonId);

        if (attendances is null)
        {
            return NotFound(
                new { message = "Lesson not found." });
        }

        return Ok(attendances);
    }

     // GET /api/lessons/{lessonId}/attendance
    // [HttpGet("/api/attendance/{attendanceId:int}/users")]
    // public async Task<IActionResult> GetUserByAttendanceId(int lessonId)
    // {
    //     var attendances =
    //         await _attendanceService
    //             .GetByLessonIdAsync(lessonId);

    //     if (attendances is null)
    //     {
    //         return NotFound(
    //             new { message = "Lesson not found." });
    //     }

    //     return Ok(attendances);
    // }
}