using CourseService.Application.DTOs.Common;
using CourseService.Domain.Entities;

namespace CourseService.Application.Interfaces.Repositories;

public interface ICourseRepository : IBaseRepository<Course>
{
    Task<Course?> GetCourseStructureAsync(Guid courseId, CancellationToken cancellationToken);
    
    Task<PagedResult<Course>> GetPublishedCoursesAsync(
        PageRequest request,
        CancellationToken cancellationToken); 
    
    Task<PagedResult<Course>> GetAuthorCoursesAsync(
        Guid authorId,
        PageRequest request,
        CancellationToken cancellationToken);
    
    Task<PagedResult<Course>> GetStudentCoursesAsync(
        Guid studentId,
        PageRequest request,
        CancellationToken cancellationToken);
}
