namespace CourseService.Application.DTOs.ModuleDtos;

public record ModuleSummaryDto(
    Guid Id,
    Guid CourseId,
    string Title,
    int Order
);
