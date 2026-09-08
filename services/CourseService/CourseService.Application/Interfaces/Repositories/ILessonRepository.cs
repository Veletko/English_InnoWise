using CourseService.Domain.Entities;

namespace CourseService.Application.Interfaces.Repositories;

public interface ILessonRepository : IBaseRepository<Lesson>
{
    Task<Lesson?> GetLessonWithTaskAsync(Guid lessonId, CancellationToken cancellationToken);
}
