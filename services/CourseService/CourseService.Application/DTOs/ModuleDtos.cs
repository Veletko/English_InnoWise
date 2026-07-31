namespace CourseService.Application.DTOs;

public record CreateModuleDto(
    Guid CourseId,
    string Title,
    int Order
);

public record UpdateModuleDto(
    string Title,
    int Order
);

public record ModuleDto(
    Guid Id,
    Guid CourseId,
    string Title,
    int Order,
    List<LessonSummaryDto> Lessons
);

public record ModuleSummaryDto(
    Guid Id,
    Guid CourseId,
    string Title,
    int Order
);
