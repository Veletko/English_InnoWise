using CourseService.Domain.Enums;
using CourseService.Domain.Payloads;

namespace CourseService.Domain.Entities;

public class CourseTask : BaseEntity
{
    public required Guid LessonId { get; set; }
    public required string Title { get; set; }
    public required TaskType Type { get; set; }
    public required int Order { get; set; }
    public required int MaxScore { get; set; }
    public required TaskPayload Payload { get; set; }
}
