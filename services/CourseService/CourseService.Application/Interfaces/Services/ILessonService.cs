using CourseService.Application.DTOs;
using CourseService.Application.DTOs.LessonDtos;

namespace CourseService.Application.Interfaces.Services;

public interface ILessonService
{
    Task<Guid> CreateLessonAsync(
        Guid authorId,
        CreateLessonDto createLessonDto,
        CancellationToken cancellationToken);

    Task UpdateLessonAsync(
        Guid lessonId,
        Guid userId,
        UpdateLessonDto updateLessonDto,
        CancellationToken cancellationToken);

    Task DeleteLessonAsync(
        Guid lessonId,
        Guid userId,
        CancellationToken cancellationToken);

    Task<LessonDetailsDto?> GetLessonByIdAsync(
        Guid lessonId,
        CancellationToken cancellationToken);

    Task<IEnumerable<LessonSummaryDto>> GetLessonsByModuleIdAsync(
        Guid moduleId,
        CancellationToken cancellationToken);
}
