using CourseService.Domain.Enums;

namespace CourseService.Application.DTOs;

public record CreateCourseDto(
    string Title,
    string? Description,
    CourseLevel Level
);

public record UpdateCourseDto(
    string Title,
    string? Description,
    CourseLevel Level,
    bool IsPublished
);

public record CourseSummaryDto(
    Guid Id,
    string Title,
    string? Description,
    CourseLevel Level,
    Guid AuthorId,
    bool IsPublished,
    DateTimeOffset CreatedAt
);

public record CourseDetailsDto(
    Guid Id,
    string Title,
    string? Description,
    CourseLevel Level,
    Guid AuthorId,
    bool IsPublished,
    DateTimeOffset CreatedAt,
    List<ModuleSummaryDto> Modules
);

public record StudentCourseDto(
    Guid Id,
    string Title,
    string? Description,
    CourseLevel Level,
    CourseStatus Status,
    double ProgressPercentage
);
