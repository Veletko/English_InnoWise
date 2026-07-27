using System.Text.Json.Nodes;

namespace CourseService.Domain.Entities;

public class TaskSubmission : BaseEntity
{
    public required Guid TaskId { get; set; }
    public required Guid UserId { get; set; }
    public required string UserAnswer { get; set; } = string.Empty;
    public int? Score { get; set; }
    public bool? IsCorrect { get; set; }
    public string? TeacherFeedback { get; set; }
}
