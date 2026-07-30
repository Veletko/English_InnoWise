namespace CourseService.Application.DTOs;

public record SubmitTaskDto(
    string UserAnswer
);

public record GradeSubmissionDto(
    int Score, 
    bool IsCorrect, 
    string? TeacherFeedback
);

public record TaskSubmissionDto(
    Guid Id, 
    Guid TaskId, 
    Guid UserId, 
    string UserAnswer, 
    int? Score, 
    bool? IsCorrect, 
    string? TeacherFeedback, 
    DateTimeOffset CreatedAt
);
