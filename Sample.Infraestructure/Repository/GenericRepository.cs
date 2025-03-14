using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.CustomAttributes;
using Sample.Domain.Models;
using Sample.Domain.ValueObjects;
using Sample.Infraestructure._shared;
using Sample.Infraestructure.Data.AdoDbContext;
using Sample.Infraestructure.Data.EFDbContext;
using System.Data;
using System.Linq.Expressions;

namespace Sample.Infraestructure.Repository
{
    public class GenericRepository<T, TKey> : IRepository<T,TKey> 
        where T : class, IEntity<TKey> where TKey : notnull
    {
        private readonly AppDbContext _efContext;
        private readonly DbSet<T> _dbSet;
        private readonly OracleDataContext _oracleContext;
        private readonly string _tableName = Attribute.GetCustomAttribute(typeof(T), typeof(EntityName))!.ToString()!;

        public GenericRepository(AppDbContext efContext, OracleDataContext oracleContext)
        {
            _efContext = efContext;
            _oracleContext = oracleContext;
            _dbSet = _efContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdateDate = null;
            await _dbSet.AddAsync(entity);
            await _efContext.SaveChangesAsync();
        }
        public async Task CreateAsync(T entity, DatabaseType flag)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdateDate = null;
            string collumns = string.Join(", ", typeof(T).GetProperties().Select(property => property.Name));
            string parameters = AdoExtension<T, TKey>.GetParameters(entity);
            string insertQuery = $"INSERT INTO {_tableName} ({collumns}) VALUES ({parameters})";
            using OracleCommand command = _oracleContext.CreateCommand(insertQuery);
            await command.ExecuteNonQueryAsync();
        }
        public async Task DeleteAsync(TKey entity)
        {
            var result = await FindByIdAsync(entity) ?? throw new Exception($"{nameof(T)} Not Found");
            _dbSet.Remove(result);
            await _efContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(string property, TKey id)
        {
            string deleteQuery = $"DELETE FROM {_tableName} WHERE {property} = '{id}'";

            using OracleCommand command = _oracleContext.CreateCommand(deleteQuery);

            await command.ExecuteNonQueryAsync();
        }
        public async Task<Dictionary<string, object>> ExcecuteProcedureOutputParams(string package,
            Dictionary<string, object>? @params = null, Dictionary<string, OracleDbType>? outputParams = null)
        {
            Dictionary<string, object> outputResults = [];

            using OracleCommand command = _oracleContext.CreateCommand(package);

            command.CommandType = CommandType.StoredProcedure;

            if (@params != null)
                foreach (var param in @params)
                    command.Parameters.Add(param.Key, param.Value);

            if (outputParams != null)
                foreach (var param in outputParams)
                {
                    OracleParameter outputParam = new()
                    {
                        ParameterName = param.Key,
                        OracleDbType = param.Value,
                        Direction = ParameterDirection.Output,
                        Size = 255
                    };
                    command.Parameters.Add(outputParam);
                }

            await command.ExecuteReaderAsync();

            if (outputParams != null)
                foreach (var param in outputParams.Keys)
                    outputResults[param] = command.Parameters[param].Value;

            return outputResults;
        }
        public async Task<List<T>> ExecuteStoredProcedureWithCursorAsync<T>(
        string procedureName) where T : new()
        {
            List<T> resultSet = [];
            using OracleCommand command = _oracleContext.CreateCommand(procedureName);
            command.CommandType = CommandType.StoredProcedure;

            OracleParameter cursorParam = new()
            {
                ParameterName = "p_cursor",
                OracleDbType = OracleDbType.RefCursor,
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(cursorParam);

            using OracleDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                T entity = new();

                foreach (var prop in typeof(T).GetProperties())
                {
                    object? value = reader[prop.Name] == DBNull.Value ? null : reader[prop.Name];
                    prop.SetValue(entity, value);
                }
                resultSet.Add(entity);
            }
            return resultSet;
        }
        public async Task<T> ExecuteFunctionAsync<T>(string functionName, Dictionary<string, object> parameters)
        {
            using OracleCommand command = _oracleContext.CreateCommand(functionName);
            command.CommandType = CommandType.Text;
            string paramList = string.Join(", ", parameters.Keys.Select(k => ":" + k));
            command.CommandText = $"SELECT {functionName}({paramList}) FROM DUAL";

            foreach (var param in parameters)
                command.Parameters.Add(new OracleParameter(param.Key, param.Value));


            object? result = await command.ExecuteScalarAsync();
            return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
        }
        public async Task<List<T>> ExecuteViewAsync<T>(string viewName) where T : new()
        {
            List<T> resultSet = [];

            using OracleCommand command = _oracleContext.CreateCommand($"SELECT * FROM {viewName}");
            command.CommandType = CommandType.Text;

            using OracleDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                T entity = new();

                foreach (var prop in typeof(T).GetProperties())
                {
                    object value = reader[prop.Name] == DBNull.Value ? null : reader[prop.Name];
                    prop.SetValue(entity, value);
                }

                resultSet.Add(entity);
            }

            return resultSet;
        }
        public async Task<T> FindByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id) ?? throw new Exception($"{nameof(T)} Not Found");
        }
        public async Task<T> FindByIdAsync(string Property, TKey id)
        {
            T? entity = default;

            string selectQuery = $"SELECT * FROM {_tableName} WHERE {Property} = '{id}'";
            using OracleCommand command = _oracleContext.CreateCommand(selectQuery);
            using OracleDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                entity = AdoExtension<T,TKey>.MapEntity(reader);

            return entity!;
        }
        public async Task<T> GetByFilter(Expression<Func<T, bool>> filter)
        {
            var entity = await _dbSet.FindAsync(filter) ?? throw new Exception($"{nameof(T)} Not Found");
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
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync() ?? throw new Exception($"{nameof(T)} List Not Found");
        }
        public async Task<List<T>> GetAllAsync(DatabaseType flag)
        {
            List<T> values = [];

            string selectQuery = $"SELECT * FROM {_tableName}";

            using OracleCommand command = _oracleContext.CreateCommand(selectQuery);
            using OracleDataReader reader = await command.ExecuteReaderAsync();
            while (reader.Read())
                values.Add(AdoExtension<T, TKey>.MapEntity(reader));
            return values;
        }
        public async Task UpdateAsync(T entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbSet.Update(entity);
            await _efContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity, string pKProperty, TKey id)
        {
            entity.UpdateDate = DateTime.Now;

            string updateQuery = $"UPDATE {_tableName} SET {AdoExtension<T, TKey>.MapSetClause(entity)} WHERE {pKProperty} = '{id}'";

            using OracleCommand command = _oracleContext.CreateCommand(updateQuery);

            await command.ExecuteNonQueryAsync();
        }
    }
}
