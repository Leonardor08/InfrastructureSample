using Oracle.ManagedDataAccess.Client;
using Sample.Domain.ValueObjects;
using System.Linq.Expressions;

namespace Sample.Application.Interfaces.Repositories;

public interface IRepository<T,TKey> where TKey : notnull
{
    Task CreateAsync(T entity);
    Task CreateAsync(T entity, DatabaseType type);
    Task DeleteAsync(TKey entity);
    Task DeleteAsync(string property, TKey id);
    Task<Dictionary<string, object>> ExcecuteProcedureOutputParams(string package, Dictionary<string, object>? values = null, Dictionary<string, OracleDbType>? outputParams = null);
    Task<List<T>> ExecuteStoredProcedureWithCursorAsync<T>(string procedureName) where T : new();
    Task<T> ExecuteFunctionAsync<T>(string functionName, Dictionary<string, object> parameters);
    Task<List<T>> ExecuteViewAsync<T>(string viewName) where T : new();
    Task<T> FindByIdAsync(TKey id);
    Task<T> FindByIdAsync(string Property, TKey id);
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(DatabaseType type);
    Task<T> GetByFilter(Expression<Func<T, bool>> filter);
    IEnumerable<T> GetByFilterOrdered(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, bool? isDesc = true);
    Task UpdateAsync(T entity);
    Task UpdateAsync(T entity, string property, TKey id);

}
