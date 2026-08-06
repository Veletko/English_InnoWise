namespace CourseService.Application.DTOs.LessonDtos;

public record CreateLessonDto(
    Guid ModuleId,
    string Title,
    string Content,
    int Order
);
