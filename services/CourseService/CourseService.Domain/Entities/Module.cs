namespace CourseService.Domain.Entities;

public class Module
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public required string Title { get; set; }
    public int Order { get; set; }
}
