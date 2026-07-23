using CourseService.Domain.Enums;

namespace CourseService.Domain.Entities;

public class Subscription
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
    public CourseStatus Status { get; set; }
    public DateTimeOffset SubscribedAt { get; set; }
}
