namespace CourseService.Application.Interfaces;

public interface ISubscriptionService
{
    Task SubscribeAsync(
        Guid userId, 
        Guid courseId, 
        CancellationToken cancellationToken = default);
    
    Task UnsubscribeAsync(
        Guid userId, 
        Guid courseId, 
        CancellationToken cancellationToken = default);
    
    Task<bool> IsSubscribedAsync(
        Guid userId, 
        Guid courseId, 
        CancellationToken cancellationToken = default);
}
