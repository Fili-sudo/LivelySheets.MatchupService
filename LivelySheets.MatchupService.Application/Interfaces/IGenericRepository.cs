using LivelySheets.MatchupService.Domain.Entities;

namespace LivelySheets.MatchupService.Application.Interfaces;

public interface IGenericRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}
