using CourseService.Domain.Entities;

namespace CourseService.Application.Interfaces.Repositories;

public interface ISubscriptionRepository : IBaseRepository<Subscription>
{ 
    Task<IEnumerable<Subscription>> GetUserSubscriptionsAsync(Guid userId, CancellationToken cancellationToken);
}
