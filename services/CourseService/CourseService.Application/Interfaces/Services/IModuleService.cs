using CourseService.Application.DTOs;

namespace CourseService.Application.Interfaces;

public interface IModuleService
{

    Task<ModuleDto> CreateModuleAsync(
        Guid authorId, 
        Guid courseId, 
        CreateModuleDto dto, 
        CancellationToken cancellationToken = default);
    
    Task<ModuleDto> UpdateModuleAsync(
        Guid authorId, 
        Guid moduleId, 
        UpdateModuleDto dto, 
        CancellationToken cancellationToken = default);
    
    Task DeleteModuleAsync(
        Guid authorId, 
        Guid moduleId, 
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<ModuleSummaryDto>> GetModulesByCourseIdAsync(
        Guid courseId, 
        CancellationToken cancellationToken = default);

    Task<ModuleDto?> GetModuleByIdAsync(
        Guid moduleId,
        CancellationToken cancellationToken = default);
}
