using CourseService.Domain.Entities;

namespace CourseService.Application.Interfaces.Repositories;

public interface ITaskSubmissionRepository : IBaseRepository
{
    Task<IEnumerable<TaskSubmission>> GetUserTaskSubmissionsAsync(Guid userId, Guid taskId, CancellationToken cancellationToken);
    Task<IEnumerable<Subscription>> GetUserSubmissionsForCourseAsync(Guid userId, Guid courseId, CancellationToken cancellationToken);
    Task<IEnumerable<Subscription>> GetSubmissionsToGradeAsync(Guid courseId, CancellationToken cancellationToken);
}

