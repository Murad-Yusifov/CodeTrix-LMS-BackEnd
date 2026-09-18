using BackEndCodeTrix.Src.Group;
using BackEndCodeTrix.Src.LessonSchedule;

namespace BackEndCodeTrix.Src.Lesson;

public interface ILessonRepository
{
    Task<List<LessonModel>> GetAllLessonsAsync();

    Task<LessonModel?> GetSingleLessonByIdAsync(int id);

    Task<LessonModel> CreateLessonAsync(
        LessonModel lesson
    );

    Task CreateAttendanceForLessonAsync(
        int lessonId,
        int groupId
    );

    Task<LessonModel?> UpdateLessonAsync(
        int id,
        LessonModel lesson
    );

    Task<bool> DeleteLessonAsync(int id);

    Task<GroupModel?> GetGroupByLessonIdAsync(
        int lessonId
    );
}