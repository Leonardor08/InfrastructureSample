using Microsoft.EntityFrameworkCore;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Infraestructure.Data.EFDbContext;
using System.Linq.Expressions;

namespace Sample.Infraestructure.Repository;

public class SqlGenericRepository<T, TKey> : ISqlRepository<T, TKey>
    where T : class, IEntity<TKey>, new()
    where TKey : notnull
{
    private readonly AppDbContext _efContext;
    private readonly DbSet<T> _dbSet;

    public SqlGenericRepository(AppDbContext efContext)
    {
        _efContext = efContext;
        _dbSet = _efContext.Set<T>();
    }
    public async Task CreateAsync(T entity)
    {
        //entity.CreatedDate = DateTime.Now;
        //entity.UpdateDate = null;
        await _dbSet.AddAsync(entity);
    }
    public async Task DeleteAsync(TKey entity)
    {
        T? result = await FindByIdAsync(entity) ?? 
            throw new Exception($"{nameof(T)} Not Found");
        _dbSet.Remove(result);
        await _efContext.SaveChangesAsync();
    }
    public async Task<T> FindByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id) ?? 
            throw new Exception($"{nameof(T)} Not Found");
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync() ?? 
            throw new Exception($"{nameof(T)} List Not Found");
    }
    public async Task<T> GetByFilter(Expression<Func<T, bool>> filter)
    {
        T? entity = await _dbSet.FindAsync(filter) ?? 
            throw new Exception($"{nameof(T)} Not Found");
        return entity;
    }
    public IEnumerable<T> GetByFilterOrdered(Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>> orderBy, bool? isDesc = true)
    {
        if (isDesc == false)
            return _dbSet.Where(predicate).OrderBy(orderBy);
        else
            return _dbSet.Where(predicate).OrderByDescending(orderBy);
    }

	public Task SaveAsync()
        => _efContext.SaveChangesAsync();

	public IQueryable<T> GetQueryablel()
    {
        _dbSet.AsQueryable();
        return _dbSet;
    }
    public async Task UpdateAsync(T entity)
    {
        entity.UpdateDate = DateTime.Now;
        _dbSet.Update(entity);
        await _efContext.SaveChangesAsync();
    }
}
