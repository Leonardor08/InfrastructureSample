using Azure.Core;
using Oracle.ManagedDataAccess.Client;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Infraestructure.Data.AdoDbContext;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Sample.Infraestructure.Repository
{
    public class GenericAdoRepository<T>(OracleDataContext context) : IAdoRepository<T> where T : Entity
    {
        private readonly OracleDataContext _context = context;
        private readonly string _tableName = typeof(T).Name;

        public async Task CreateAsync(T entity)
        {
            try
            {
                _context.BeginTransaction();

                string insertQuery = $"INSERT INTO {_tableName} ({GenericAdoRepository<T>.GetColumns()}) VALUES ({GenericAdoRepository<T>.GetParameters()})";

                using OracleCommand command = _context.CreateCommand(insertQuery);

                await command.ExecuteNonQueryAsync();

                _context.Commit();

                _context.Dispose();
            }
            catch (Exception)
            {
                _context.Rollback();
                _context.Dispose();
                throw;
            }
        }

        public List<T> ReadAllAsync()
        {
            List<T> list = [];
            try
            {
                _context.BeginTransaction();

                string selectQuery = $"SELECT * FROM {_tableName})";

                using OracleCommand command = _context.CreateCommand(selectQuery);

                using OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    list.Add(GenericAdoRepository<T>.MapEntity(reader));

                _context.Dispose();

                return list;
            }
            catch (Exception)
            {
                _context.Rollback();
                _context.Dispose();
                throw;
            }
        }

        public T FindByIdAsync(string Property, Guid id)
        {
            T? entity = default;

            try
            {
                _context.BeginTransaction();

                string selectQuery = $"SELECT * FROM {_tableName} WHERE {Property} = {id}";

                using OracleCommand command = _context.CreateCommand(selectQuery);

                using OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    entity = GenericAdoRepository<T>.MapEntity(reader);

                _context.Dispose();

                return entity!;
            }
            catch (Exception)
            {
                _context.Rollback();
                _context.Dispose();
                throw;
            }
        }

        public async Task UpdateAsync(T entity, string pKProperty, Guid id)
        {
            Dictionary<string, string> keyValuePairs = [];
            try
            {
                _context.BeginTransaction();

                string updateQuery = $"UPDATE {_tableName} SET {MapSetClause()} WHERE {pKProperty} = {id}";

                using OracleCommand command = _context.CreateCommand(updateQuery);

                await command.ExecuteNonQueryAsync();

                _context.Commit();

                _context.Dispose();
            }
            catch (Exception)
            {
                _context.Rollback();
                _context.Dispose();
                throw;
            }
        }

        public async Task DeleteAsync(string property, Guid id)
        {
            try
            {
                _context.BeginTransaction();

                string deleteQuery = $"DELETE * FROM {_tableName} WHERE {property} = {id}";

                using OracleCommand command = _context.CreateCommand(deleteQuery);

                await command.ExecuteNonQueryAsync();

                _context.Commit();

                _context.Dispose();
            }
            catch (Exception)
            {
                _context.Rollback();
                _context.Dispose();
                throw;
            }
        }                

        private static string GetColumns()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            return string.Join(", ", properties.Select(propertyInfo => propertyInfo.Name));
        }

        private static string GetParameters()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            return string.Join(", ", properties.Select(propertyInfo => $":{propertyInfo.Name}"));
        }

        private static T MapEntity(OracleDataReader reader)
        {
            T entity = Activator.CreateInstance<T>();

            foreach (PropertyInfo prop in typeof(T).GetProperties())
                if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                    prop.SetValue(entity, reader[prop.Name]);

            return entity;
        }        

        private static string MapSetClause()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            return string.Join(" ,", properties.Select(propertyInfo => $"{propertyInfo.Name} = :{propertyInfo.Name}"));
        }
    }
}
