using AutoMapper;

using BackEndCodeTrix.Src.Data;
using BackEndCodeTrix.Src.Group.GroupDTO;
using BackEndCodeTrix.Src.Lesson.LessonDTO;
using BackEndCodeTrix.Src.LessonSchedule;
using Microsoft.EntityFrameworkCore;

namespace BackEndCodeTrix.Src.Lesson;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _lessonRepository;

    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;

    public LessonService(
        ILessonRepository lessonRepository,
        ApplicationDbContext context,
        IMapper mapper
    )
    {
        _lessonRepository = lessonRepository;
        _context = context;
        _mapper = mapper;
    }


    // GET ALL
    public async Task<List<LessonResponseDto>>
        GetAllLessonsAsync()
    {
        var lessons =
            await _lessonRepository.GetAllLessonsAsync();

        return _mapper.Map<List<LessonResponseDto>>(
            lessons
        );
    }


    // GET BY ID
    public async Task<LessonResponseDto?>
        GetSingleLessonByIdAsync(int id)
    {
        var lesson =
            await _lessonRepository
                .GetSingleLessonByIdAsync(id);

        if (lesson is null)
        {
            return null;
        }

        return _mapper.Map<LessonResponseDto>(
            lesson
        );
    }


    // CREATE
    public async Task<(LessonResponseDto? Lesson, string? Error)>
        CreateLessonAsync(CreateLessonDto dto)
    {
        var groupExists =
            await _context.Groups
                .AnyAsync(g => g.GroupId == dto.GroupId);

        if (!groupExists)
        {
            return (null, "GroupNotFound");
        }

        var lesson =
            _mapper.Map<LessonModel>(dto);

        var createdLesson =
            await _lessonRepository
                .CreateLessonAsync(lesson);

        return (
            _mapper.Map<LessonResponseDto>(
                createdLesson
            ),
            null
        );
    }


    // UPDATE
    public async Task<(LessonResponseDto? Lesson, string? Error)>
        UpdateLessonAsync(
            int id,
            UpdateLessonDto dto
        )
    {
        var lessonExists =
            await _lessonRepository
                .GetSingleLessonByIdAsync(id);

        if (lessonExists is null)
        {
            return (null, "LessonNotFound");
        }

        var groupExists =
            await _context.Groups
                .AnyAsync(
                    g => g.GroupId == dto.GroupId
                );

        if (!groupExists)
        {
            return (null, "GroupNotFound");
        }

        var lesson =
            _mapper.Map<LessonModel>(dto);

        var updatedLesson =
            await _lessonRepository
                .UpdateLessonAsync(
                    id,
                    lesson
                );

        return (
            _mapper.Map<LessonResponseDto>(
                updatedLesson
            ),
            null
        );
    }


    // DELETE
    public async Task<bool> DeleteLessonAsync(
        int id
    )
    {
        return await _lessonRepository
            .DeleteLessonAsync(id);
    }

    public async Task <GroupResponseDto?> GetGroupByLessonIdAsync(int lessonId)
    {
        var group = await _lessonRepository.GetGroupByLessonIdAsync(lessonId);

        return _mapper.Map<GroupResponseDto>(group);
        
    }
}