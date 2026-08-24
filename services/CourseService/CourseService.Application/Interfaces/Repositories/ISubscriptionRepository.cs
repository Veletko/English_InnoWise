using CourseService.Domain.Entities;

namespace CourseService.Application.Interfaces.Repositories;

public interface ISubscriptionRepository : IBaseRepository
{
    Task<bool> ExistsAsync(Guid subscriptionId, CancellationToken cancellationToken);
    Task<IEnumerable<Subscription>> GetUserSubscriptionsAsync(Guid user, CancellationToken cancellationToken);
}
