namespace CourseService.Domain.Entities;

public class Module : BaseEntity
{
    public required Guid CourseId { get; set; }
    public required string Title { get; set; }
    public required int Order { get; set; }
}
