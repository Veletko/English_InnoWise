using CourseService.Application.DTOs.TaskDtos;

namespace CourseService.Application.DTOs.LessonDtos;

public record LessonDetailsDto(
    Guid Id,
    Guid ModuleId,
    string Title,
    string Content,
    int Order,
    List<TaskDto> Tasks
);
