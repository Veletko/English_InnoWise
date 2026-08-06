namespace CourseService.Application.DTOs.ModuleDtos;

public record CreateModuleDto(
    Guid CourseId,
    string Title,
    int Order
);
