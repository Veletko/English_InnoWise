using CourseService.Domain.Enums;

namespace CourseService.Domain.Entities;

public class Course : BaseEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required CourseLevel Level { get; set; }
    public required Guid AuthorId  { get; set; }
    public required bool IsPublished { get; set; }
}
