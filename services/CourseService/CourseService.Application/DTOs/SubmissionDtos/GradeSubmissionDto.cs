namespace CourseService.Application.DTOs.SubmissionDtos;

public record GradeSubmissionDto(
    int Score, 
    bool IsCorrect, 
    string? TeacherFeedback
);
