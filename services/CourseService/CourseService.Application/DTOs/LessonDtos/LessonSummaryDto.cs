namespace CourseService.Application.DTOs.LessonDtos;

public record LessonSummaryDto(
    Guid Id, 
    string Title,
    int Order
);
