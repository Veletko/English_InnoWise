using CourseService.Domain.Entities;

namespace CourseService.Application.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
