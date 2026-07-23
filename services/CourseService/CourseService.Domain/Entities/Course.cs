using CourseService.Domain.Enums;

namespace CourseService.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public CourseLevel Level { get; set; }
    public Guid AuthorId  { get; set; }
    public bool IsPublished { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
