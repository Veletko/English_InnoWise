using CourseService.Application.DTOs.ModuleDtos;
using CourseService.Domain.Enums;

namespace CourseService.Application.DTOs.CourseDtos;

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
