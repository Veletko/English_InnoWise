using CourseService.Application.DTOs;
using CourseService.Application.DTOs.SubmissionDtos;

namespace CourseService.Application.Interfaces.Services;

public interface ITaskSubmissionService
{
    Task<TaskSubmissionDto> SubmitTaskAsync(
        Guid userId,
        Guid taskId,
        SubmitTaskDto submitTaskDto,
        CancellationToken cancellationToken );
    
    Task<IEnumerable<TaskSubmissionDto>> GetUserSubmissionsAsync(
        Guid userId,
        Guid courseId, 
        CancellationToken cancellationToken );
    
    Task<IEnumerable<TaskSubmissionDto>> GetUserSubmissionsForTaskAsync(
        Guid userId,
        Guid taskId,
        CancellationToken cancellationToken );
    
    Task<TaskSubmissionDto> GradeSubmissionAsync(
        Guid teacherId,
        Guid submissionId,
        GradeSubmissionDto gradeSubmissionDto,
        CancellationToken cancellationToken );
    
    Task<IEnumerable<TaskSubmissionDto>> GetSubmissionsToGradeAsync(
        Guid teacherId, 
        Guid courseId, 
        CancellationToken cancellationToken );
}
