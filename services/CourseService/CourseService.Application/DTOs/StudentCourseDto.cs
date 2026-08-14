using CourseService.Domain.Enums;

namespace CourseService.Application.DTOs;

public record StudentCourseDto(
    Guid Id,
    string Title,
    string? Description,
    CourseLevel Level,
    CourseStatus Status,
    double ProgressPercentage
);
