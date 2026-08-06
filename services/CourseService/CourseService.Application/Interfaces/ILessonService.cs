using CourseService.Application.DTOs;

namespace CourseService.Application.Interfaces;

public interface ILessonService
{
    Task<LessonDetailsDto> CreateLessonAsync(
        Guid authorId,
        Guid moduleId,
        CreateLessonDto createLessonDto,
        CancellationToken cancellationToken = default
    );

    Task<LessonDetailsDto> UpdateLessonAsync(
        Guid authorId,
        Guid lessonId,
        UpdateLessonDto updateLessonDto,
        CancellationToken cancellationToken = default);

    Task DeleteLessonAsync(
        Guid authorId,
        Guid lessonId,
        CancellationToken cancellationToken = default
    );

    Task<LessonDetailsDto?> GetLessonByIdAsync(
        Guid lessonId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<LessonSummaryDto>> GetLessonsByModuleIdAsync(
        Guid moduleId,
        CancellationToken cancellationToken = default);
}
