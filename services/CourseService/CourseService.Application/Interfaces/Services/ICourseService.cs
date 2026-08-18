using CourseService.Application.Constants;
using CourseService.Application.DTOs;
using CourseService.Application.DTOs.CourseDtos;

namespace CourseService.Application.Interfaces.Services;

public interface ICourseService
{
    Task<Guid> CreateCourseAsync(
        Guid authorId,
        CreateCourseDto createCourseDto,
        CancellationToken cancellationToken );
    
    Task UpdateCourseAsync(
        Guid courseId,
        Guid userId,
        UpdateCourseDto updateCourseDto,
        CancellationToken cancellationToken );
    
    Task DeleteCourseAsync(
        Guid courseId,
        Guid userId,
        CancellationToken cancellationToken );
    
    Task<IEnumerable<CourseSummaryDto>> GetAuthorCoursesAsync(
        Guid authorId,
        CancellationToken cancellationToken,
        int page = PageConsts.DefaultPageNumber, 
        int pageSize = PageConsts.DefaultPageSize);
    
    Task<CourseDetailsDto?> GetCourseByIdAsync(
        Guid courseId, 
        CancellationToken cancellationToken );
    
    Task<IEnumerable<CourseSummaryDto>> GetPublishedCoursesAsync(
        CancellationToken cancellationToken,
        int page = PageConsts.DefaultPageNumber, 
        int pageSize = PageConsts.DefaultPageSize);
    
    Task<IEnumerable<StudentCourseDto>> GetStudentCoursesAsync(
        Guid studentId, 
        CancellationToken cancellationToken,
        int page = PageConsts.DefaultPageNumber, 
        int pageSize = PageConsts.DefaultPageSize);

    Task<StudentCourseDto?> GetStudentCourseContentAsync(
        Guid studentId, 
        Guid courseId, 
        CancellationToken cancellationToken );
}
