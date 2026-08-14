using CourseService.Domain.Enums;

namespace CourseService.Application.DTOs.CourseDtos;

public record UpdateCourseDto(
    string? Title,
    string? Description,
    CourseLevel? Level,
    bool? IsPublished
);
