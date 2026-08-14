namespace CourseService.Application.DTOs.LessonDtos;

public record UpdateLessonDto(
    string? Title,
    string? Content,
    int? Order
);
