using CourseService.Application.DTOs.LessonDtos;

namespace CourseService.Application.DTOs.ModuleDtos;

public record ModuleDetailsDto(
    Guid Id,
    Guid CourseId,
    string Title,
    int Order,
    List<LessonSummaryDto> Lessons
);


