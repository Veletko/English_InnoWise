using System.Text.Json.Nodes;
using CourseService.Domain.Enums;
namespace CourseService.Domain.Entities;

public class CourseTask
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public required string Title { get; set; }
    public TaskType Type { get; set; }
    public int Order { get; set; }
    public int MaxScore { get; set; }
    public string Content { get; set; } = string.Empty;
}
