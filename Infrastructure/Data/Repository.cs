using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data;

public interface IRepository<T> where T : class, IBaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null);
    IQueryable<T> Queryable();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}

public class Repository<T> : IRepository<T> where T : class, IBaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _entity;

    public Repository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _entity = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _entity.FindAsync(id);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await _entity.FirstOrDefaultAsync(expression);
    }

    public IQueryable<T> Queryable() => _entity.AsQueryable();

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null)
    {
        var query = _entity.AsQueryable();

        if (expression is not null)
        {
            query = query.Where(expression);
        }

        return await query.ToListAsync();
    }


    public async Task AddAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await _entity.AddAsync(entity);
    }

    public void Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _entity.Update(entity);
    }

    public void Delete(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _entity.Remove(entity);
    }

}
