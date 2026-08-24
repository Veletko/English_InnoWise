using CourseService.Application.Constants;
using CourseService.Domain.Entities;

namespace CourseService.Application.Interfaces.Repositories;

public interface ICourseRepository : IBaseRepository
{
    Task<Course?> GetCourseStructureAsync(Guid courseId, CancellationToken cancellationToken);
    
    Task<(IEnumerable<Course> Items, int TotalCount)> GetPublishedCoursesAsync(
        CancellationToken cancellationToken,
        int page = PageConsts.DefaultPageNumber,
        int pageSize = PageConsts.DefaultPageSize ); 
    
    Task<(IEnumerable<Course> Items, int TotalCount)> GetAuthorCoursesAsync(
        CancellationToken cancellationToken,
        Guid authorId,
        int page = PageConsts.DefaultPageNumber,
        int pageSize = PageConsts.DefaultPageSize);
    
    Task<(IEnumerable<Course> Items, int TotalCount)> GetStudentCoursesAsync(
        CancellationToken cancellationToken,
        Guid studentId,
        int page = PageConsts.DefaultPageNumber,
        int pageSize = PageConsts.DefaultPageSize);
    
    Task<IEnumerable<Module>> GetModulesByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Lesson>> GetLessonsByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Task>> GetCourseTasksByLessonIdAsync(Guid lessonId, CancellationToken cancellationToken);
}
