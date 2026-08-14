namespace CourseService.Application.DTOs.ModuleDtos;

public record UpdateModuleDto(
    string? Title,
    int? Order
);
