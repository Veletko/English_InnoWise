using CourseService.Application.DTOs;

namespace CourseService.Application.Interfaces;

public interface ICourseService
{
    Task<CourseDetailsDto> CreateCourseAsync(
        Guid authorId,
        CreateCourseDto createCourseDto,
        CancellationToken cancellationToken = default);
    
    Task<CourseDetailsDto> UpdateCourseAsync(
        Guid authorId,
        Guid courseId,
        UpdateCourseDto updateCourseDto,
        CancellationToken cancellationToken = default);
    
    Task DeleteCourseAsync(
        Guid authorId,
        Guid courseId,
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<CourseSummaryDto>> GetAuthorCoursesAsync(
        Guid authorId,
        CancellationToken cancellationToken = default);
    
    Task<CourseDetailsDto?> GetCourseByIdAsync(
        Guid courseId, 
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<CourseSummaryDto>> GetPublishedCoursesAsync(
        int page = 1, 
        int pageSize = 10, 
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<StudentCourseDto>> GetStudentCoursesAsync(
        Guid userId, 
        CancellationToken cancellationToken = default);

    Task<CourseDetailsDto?> GetStudentCourseContentAsync(
        Guid userId, 
        Guid courseId, 
        CancellationToken cancellationToken = default);
}
