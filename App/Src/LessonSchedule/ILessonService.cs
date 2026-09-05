using BackEndCodeTrix.Src.Group.GroupDTO;
using BackEndCodeTrix.Src.Lesson.LessonDTO;

namespace BackEndCodeTrix.Src.Lesson;

public interface ILessonService
{
    Task<List<LessonResponseDto>>
        GetAllLessonsAsync();

    Task<LessonResponseDto?>
        GetSingleLessonByIdAsync(int id);

    Task<(LessonResponseDto? Lesson, string? Error)>
        CreateLessonAsync(CreateLessonDto dto);

    Task<(LessonResponseDto? Lesson, string? Error)>
        UpdateLessonAsync(
            int id,
            UpdateLessonDto dto
        );

    Task<bool> DeleteLessonAsync(int id);

    Task <GroupResponseDto?> GetGroupByLessonIdAsync(int lessonId);
}