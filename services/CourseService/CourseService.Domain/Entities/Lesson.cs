namespace CourseService.Domain.Entities;

public class Lesson : BaseEntity
{
    public required Guid ModuleId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required int Order { get; set; }
    public ICollection<CourseTask> Tasks { get; set; } = new List<CourseTask>();
}
