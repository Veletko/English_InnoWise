namespace CourseService.Application.DTOs;

public record CreateLessonDto(
    Guid ModuleId,
    string Title,
    string Content,
    int Order
);

public record UpdateLessonDto(
    string Title,
    string Content,
    int Order
);

public record LessonSummaryDto(
    Guid Id, 
    string Title,
    int Order
);

public record LessonDetailsDto(
    Guid Id,
    Guid ModuleId,
    string Title,
    string Content,
    int Order,
    List<TaskDto> Tasks
);
