using CourseService.Domain.Enums;

namespace CourseService.Application.DTOs.CourseDtos;

public record CreateCourseDto(
    string Title,
    string? Description,
    CourseLevel Level
);
