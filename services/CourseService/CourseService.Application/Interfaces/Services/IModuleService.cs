using CourseService.Application.DTOs.ModuleDtos;

namespace CourseService.Application.Interfaces.Services;

public interface IModuleService
{
    Task<Guid> CreateModuleAsync(
        Guid authorId, 
        CreateModuleDto createModuleDto, 
        CancellationToken cancellationToken);
    
    Task UpdateModuleAsync(
        Guid moduleId, 
        Guid userId,
        UpdateModuleDto updateModuleDto, 
        CancellationToken cancellationToken);
    
    Task DeleteModuleAsync(
        Guid moduleId, 
        Guid userId,
        CancellationToken cancellationToken);
    
    Task<IEnumerable<ModuleSummaryDto>> GetModulesByCourseIdAsync(
        Guid courseId, 
        CancellationToken cancellationToken);

    Task<ModuleDto?> GetModuleByIdAsync(
        Guid moduleId,
        CancellationToken cancellationToken);
}
