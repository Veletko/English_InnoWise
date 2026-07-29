using CourseService.Domain.Enums;

namespace CourseService.Domain.Entities;

public class Subscription : BaseEntity
{
    public required Guid CourseId { get; set; }
    public required Guid UserId { get; set; }
    public required CourseStatus Status { get; set; }
}
