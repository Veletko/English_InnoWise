using CourseService.Application.DTOs.TaskDtos;

namespace CourseService.Application.Interfaces.Services;

public interface ITaskService
{
    Task<Guid> CreateTaskAsync(
        Guid authorId,
        CreateTaskDto createTaskDto,
        CancellationToken cancellationToken);

    Task UpdateTaskAsync(
        Guid taskId,
        Guid userId,
        UpdateTaskDto updateTaskDto,
        CancellationToken cancellationToken);

    Task DeleteTaskAsync(
        Guid taskId,
        Guid userId,
        CancellationToken cancellationToken);
    
    Task<IEnumerable<TaskDto>> GetTasksByLessonIdAsync(
        Guid lessonId,
        CancellationToken cancellationToken);
    
    Task<TaskDto?> GetTaskByIdAsync(
        Guid taskId,
        CancellationToken cancellationToken);
}
