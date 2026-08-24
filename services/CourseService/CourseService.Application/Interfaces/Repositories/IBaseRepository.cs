using CourseService.Domain.Entities;

namespace CourseService.Application.Interfaces.Repositories;

public interface IBaseRepository
{
    Task<TEntity?> GetByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : BaseEntity;
    Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseEntity;
    void Update<TEntity>(TEntity entity) where TEntity : BaseEntity;
    void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity;
}
