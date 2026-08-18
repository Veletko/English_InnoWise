namespace CourseService.Application.DTOs.SubmissionDtos;

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
