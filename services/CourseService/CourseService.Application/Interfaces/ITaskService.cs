using CourseService.Application.DTOs;

namespace CourseService.Application.Interfaces;

public interface ITaskService
{
    Task<TaskDto> CreateTaskAsync(
        Guid authorId,
        Guid lessonId,
        CreateTaskDto createTaskDto,
        CancellationToken cancellationToken = default);

    Task<TaskDto> UpdateTaskAsync(
        Guid authorId,
        Guid taskId,
        UpdateTaskDto updateTaskDto,
        CancellationToken cancellationToken = default
    );

    Task DeleteTaskAsync(
        Guid authorId,
        Guid taskId,
        CancellationToken cancellationToken = default
    );
    
    Task<IEnumerable<TaskDto>> GetTasksByLessonIdAsync(
        Guid lessonId,
        CancellationToken cancellationToken = default
    );
    
    Task<TaskDto?> GetTaskByIdAsync(
        Guid taskId,
        CancellationToken cancellationToken = default);
}
