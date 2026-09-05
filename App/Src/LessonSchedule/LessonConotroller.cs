using BackEndCodeTrix.Src.Group.GroupDTO;
using BackEndCodeTrix.Src.Lesson.LessonDTO;

using Microsoft.AspNetCore.Mvc;

namespace BackEndCodeTrix.Src.Lesson;

[ApiController]
[Route("api/[controller]")]
public class LessonController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonController(
        ILessonService lessonService
    )
    {
        _lessonService = lessonService;
    }


    // GET ALL LESSONS
    [HttpGet]
    public async Task<IActionResult>
        GetAllLessons()
    {
        var lessons =
            await _lessonService.GetAllLessonsAsync();

        return Ok(lessons);
    }


    // GET LESSON BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult>
        GetLessonById(int id)
    {
        var lesson =
            await _lessonService
                .GetSingleLessonByIdAsync(id);

        if (lesson is null)
        {
            return NotFound(
                "Lesson not found."
            );
        }

        return Ok(lesson);
    }


    // CREATE LESSON
    [HttpPost]
    public async Task<IActionResult>
        CreateLesson(
            CreateLessonDto dto
        )
    {
        var result =
            await _lessonService
                .CreateLessonAsync(dto);

        if (result.Error == "GroupNotFound")
        {
            return BadRequest(
                "Group not found."
            );
        }

        return CreatedAtAction(
            nameof(GetLessonById),
            new
            {
                id = result.Lesson!.LessonId
            },
            result.Lesson
        );
    }


    // UPDATE LESSON
    [HttpPut("{id}")]
    public async Task<IActionResult>
        UpdateLesson(
            int id,
            UpdateLessonDto dto
        )
    {
        var result =
            await _lessonService
                .UpdateLessonAsync(
                    id,
                    dto
                );

        if (result.Error == "LessonNotFound")
        {
            return NotFound(
                "Lesson not found."
            );
        }

        if (result.Error == "GroupNotFound")
        {
            return BadRequest(
                "Group not found."
            );
        }

        return Ok(result.Lesson);
    }


    // DELETE LESSON
    [HttpDelete("{id}")]
    public async Task<IActionResult>
        DeleteLesson(int id)
    {
        var deleted =
            await _lessonService
                .DeleteLessonAsync(id);

        if (!deleted)
        {
            return NotFound(
                "Lesson not found."
            );
        }

        return NoContent();
    }

    // [HttpGet("{lessonId}/group")]
    // public async Task<IActionResult> GetGroupByLessonId(int lessonId)
    // {
    //     var group = await _lessonService.GetGroupByLessonIdAsync(lessonId);
    //     if(group is null)
    //     {
    //         return NotFound("Group is not found");
    //     }

    //     return Ok(group);
    // }

    // IMPORTANT: This should appear in Swagger
    [HttpGet("{lessonId}/group")]
    public async Task<IActionResult> GetGroupByLessonId(int lessonId)
    {
        var group =
            await _lessonService.GetGroupByLessonIdAsync(lessonId);

        if (group is null)
            return NotFound("Group is not found.");

        return Ok(group);
    }
}

