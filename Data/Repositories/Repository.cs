using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaxCalc.Data.Contracts;

namespace TaxCalc.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext _context;
    
    public Repository(DbContext context)
    {
        _context = context;
    }
    
    public async Task<TEntity> GetByIdAsync<TId>(TId id) where TId : struct
    {
        var result = await _context.Set<TEntity>().FindAsync(id);
        return result;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var result = await _context.Set<TEntity>().ToListAsync();
        return result;
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await _context.Set<TEntity>().Where(predicate).ToListAsync();
        return result;
    }
    
    public async Task AddAsync(TEntity entity)
    {
       await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }

    public async Task RemoveAsync(TEntity entity)
    {
        await Task.FromResult(_context.Set<TEntity>().Remove(entity));
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        await Task.Run(() => _context.Set<TEntity>().RemoveRange(entities));
    }
}