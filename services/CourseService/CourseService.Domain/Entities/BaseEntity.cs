namespace CourseService.Domain.Entities;

public class BaseEntity
{
    public required Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
