namespace CourseService.Application.Interfaces.Services;

public interface ISubscriptionService
{
    Task SubscribeAsync(
        Guid userId, 
        Guid courseId, 
        CancellationToken cancellationToken);
    
    Task UnsubscribeAsync(
        Guid userId, 
        Guid courseId, 
        CancellationToken cancellationToken);
    
    Task<bool> IsSubscribedAsync(
        Guid userId, 
        Guid courseId, 
        CancellationToken cancellationToken);
}
