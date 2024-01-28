using System.Collections;
using System.Linq.Expressions;

namespace TaxCalc.Data.Contracts;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync<TId>(TId id) where TId : struct;
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task RemoveAsync(TEntity entity);
    Task RemoveRangeAsync(IEnumerable<TEntity> entities);
}