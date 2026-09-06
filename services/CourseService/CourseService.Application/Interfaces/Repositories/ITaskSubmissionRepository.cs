using CourseService.Domain.Entities;

namespace CourseService.Application.Interfaces.Repositories;

public interface ITaskSubmissionRepository : IBaseRepository<TaskSubmission>
{
    Task<IEnumerable<TaskSubmission>> GetUserTaskSubmissionsAsync(Guid userId, Guid submissionId, CancellationToken cancellationToken);
    Task<IEnumerable<TaskSubmission>> GetUserSubmissionsForCourseAsync(Guid userId, Guid courseId, CancellationToken cancellationToken);
    Task<IEnumerable<TaskSubmission>> GetSubmissionsToGradeAsync(Guid courseId, CancellationToken cancellationToken);
}

