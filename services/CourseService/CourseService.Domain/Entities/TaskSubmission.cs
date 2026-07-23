using System.Text.Json.Nodes;

namespace CourseService.Domain.Entities;

public class TaskSubmission
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
    public string UserAnswer { get; set; } = string.Empty;
    public int? Score { get; set; }
    public bool? IsCorrect { get; set; }
    public string? TeacherFeedback { get; set; }
    public DateTimeOffset SubmittedAt { get; set; }
}
