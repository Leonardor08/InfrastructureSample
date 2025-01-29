using Microsoft.EntityFrameworkCore;
using Sample.Domain.Interfaces;
using Sample.Domain.Models;
using Sample.Infraestructure.Data.EFDbContext;
using System.Linq.Expressions;

namespace Sample.Infraestructure.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : Entity
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            entity.UpdateDate = null;
            await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            var result = await FindByIdAsync(entity.Id) ?? throw new Exception($"{nameof(T)} Not Found");
            _dbSet.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<T> FindByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id) ?? throw new Exception($"{nameof(T)} Not Found");
        }

        public async Task<T> GetByFilter(Expression<Func<T, bool>> filter)
        {
            var entity = await _dbSet.FindAsync(filter) ?? throw new Exception($"{nameof(T)} Not Found");
            return entity;
        }

        public IEnumerable<T> GetByFilterOrdered(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, bool? isDesc = true)
        {
            if (isDesc == false)
                return _dbSet.Where(predicate).OrderBy(orderBy);
            else
                return _dbSet.Where(predicate).OrderByDescending(orderBy);
        }

        public async Task<List<T>> ReadAllAsync()
        {
            return await _dbSet.ToListAsync() ?? throw new Exception($"{nameof(T)} List Not Found");
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbSet.Update(entity);
            await _appDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
