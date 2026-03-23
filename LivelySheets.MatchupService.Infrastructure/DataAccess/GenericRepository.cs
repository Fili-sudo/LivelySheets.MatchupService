using LivelySheets.MatchupService.Application.Interfaces;
using LivelySheets.MatchupService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LivelySheets.MatchupService.Infrastructure.DataAccess;

public class GenericRepository<T> : IGenericRepository<T> where T : Entity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _values;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _values = context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _values.AddAsync(entity, cancellationToken);
        await SaveAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entityToDelete = await _values.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entityToDelete is not null)
        {
            _values.Remove(entityToDelete);
            await SaveAsync(cancellationToken);
        }
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _values.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
