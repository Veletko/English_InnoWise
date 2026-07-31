using CourseService.Application.DTOs;

namespace CourseService.Application.Interfaces;

public interface ITaskSubmissionService
{
    Task<TaskSubmissionDto> SubmitTaskAsync(
        Guid userId,
        Guid taskId,
        SubmitTaskDto submitTaskDto,
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TaskSubmissionDto>> GetUserSubmissionsAsync(
        Guid userId,
        Guid courseId, 
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TaskSubmissionDto>> GetUserSubmissionsForTaskAsync(
        Guid userId,
        Guid taskId,
        CancellationToken cancellationToken = default);
    
    Task<TaskSubmissionDto> GradeSubmissionAsync(
        Guid teacherId,
        Guid submissionId,
        GradeSubmissionDto gradeSubmissionDto,
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<TaskSubmissionDto>> GetSubmissionsToGradeAsync(
        Guid teacherId, 
        Guid courseId, 
        CancellationToken cancellationToken = default);
}
