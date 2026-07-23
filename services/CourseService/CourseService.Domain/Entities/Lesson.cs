namespace CourseService.Domain.Entities;

public class Lesson
{
    public Guid Id { get; set; }
    public Guid ModuleId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public int Order { get; set; }
}
