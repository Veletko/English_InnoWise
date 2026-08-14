using CourseService.Domain.Enums;

namespace CourseService.Application.DTOs.CourseDtos;

public record CourseSummaryDto(
    Guid Id,
    string Title,
    CourseLevel Level,
    Guid AuthorId,
    string AuthorName,
    DateTimeOffset CreatedAt
);
