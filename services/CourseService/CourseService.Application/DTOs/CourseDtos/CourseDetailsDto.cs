using CourseService.Application.DTOs.ModuleDtos;
using CourseService.Domain.Enums;

namespace CourseService.Application.DTOs.CourseDtos;

public record CourseDetailsDto(
    Guid Id,
    string Title,
    string? Description,
    CourseLevel Level,
    Guid AuthorId,
    string AuthorName,
    bool IsPublished,
    DateTimeOffset CreatedAt,
    List<ModuleSummaryDto> Modules
);
