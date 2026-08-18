using CourseService.Domain.Enums;
using CourseService.Domain.Payloads;

namespace CourseService.Application.DTOs.TaskDtos;

public record UpdateTaskDto(
    string? Title, 
    TaskType? Type, 
    int? Order, 
    int? MaxScore, 
    TaskPayload? Payload
);
