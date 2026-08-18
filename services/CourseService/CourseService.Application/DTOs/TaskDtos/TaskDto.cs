using CourseService.Domain.Enums;
using CourseService.Domain.Payloads;

namespace CourseService.Application.DTOs.TaskDtos;

public record TaskDto(
    Guid Id, 
    Guid LessonId, 
    string Title, 
    TaskType Type, 
    int Order, 
    int MaxScore, 
    TaskPayload Payload
);

