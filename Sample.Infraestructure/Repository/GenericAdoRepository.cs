using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Oracle.ManagedDataAccess.Client;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Infraestructure.Data.AdoDbContext;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

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

				string insertQuery = $"INSERT INTO {_tableName} VALUES ({GetParameters(entity)})";

				using OracleCommand command = _context.CreateCommand(insertQuery);

				await command.ExecuteNonQueryAsync();

				_context.Commit();

				_context.Dispose();
			}
			catch (Exception)
			{
				_context.Rollback();
				throw;
			}
			finally
			{
				_context.Dispose();
			}
		}

		public async Task<List<T>> ReadAllAsync()
		{
			List<T> list = [];

			try
			{
				_context.BeginTransaction();
				string selectQuery = $"SELECT * FROM {_tableName}";
				using OracleCommand command = _context.CreateCommand(selectQuery);
				using OracleDataReader reader = await command.ExecuteReaderAsync();
				while (await reader.ReadAsync())
				{
					list.Add(MapEntity(reader));
				}
			}
			catch (Exception ex)
			{
				_context.Rollback();
				throw;
			}
			finally
			{
				_context.Dispose();
			}
			return list;
		}

		public async Task<T> FindByIdAsync(string Property, Guid id)
		{
			T? entity = default;

			try
			{
				_context.BeginTransaction();
				string selectQuery = $"SELECT * FROM {_tableName} WHERE {Property} = '{id}'";
				using OracleCommand command = _context.CreateCommand(selectQuery);
				using OracleDataReader reader = await command.ExecuteReaderAsync();
				while (await reader.ReadAsync())
				{
					entity = (MapEntity(reader));
				}
				return entity!;
			}
			catch (Exception)
			{
				_context.Rollback();
				throw;
			}
			finally
			{
				_context.Dispose();
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
				throw;
			}
			finally
			{
				_context.Dispose();
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


		private static string GetParameters(T entity)
		{
			entity.CreatedDate = DateTime.Now;
			List<object> values = [];
			PropertyInfo[] properties = typeof(T).GetProperties();

			foreach (var item in properties)
			{
				var value = item.GetValue(entity);
				if (value == null)
					values.Add("NULL");
				else if (item.PropertyType == typeof(string) || item.PropertyType == typeof(Guid) || item.PropertyType == typeof(DateTime))
					values.Add($"'{value}'");
				else
					values.Add(value);
			}
			return string.Join(", ", values);
		}

		private static T MapEntity(OracleDataReader reader)
		{
			T entity = Activator.CreateInstance<T>();
			PropertyInfo[] properties = typeof(T).GetProperties();

			for (int i = 0; i < properties.Length; i++)
			{
				if (reader[properties[i].Name] == DBNull.Value)
				{
					properties[i].SetValue(entity, null);
				}
				else
				{
					object value = reader[properties[i].Name];

					if (properties[i].PropertyType == typeof(Guid))
						properties[i].SetValue(entity, Guid.Parse(value.ToString()!));
					else
						properties[i].SetValue(entity, Convert.ChangeType(value, Nullable.GetUnderlyingType(properties[i].PropertyType) ?? properties[i].PropertyType));
				}
			}

			return entity;
		}
		private static string MapSetClause()
		{
			PropertyInfo[] properties = typeof(T).GetProperties();
			return string.Join(" ,", properties.Select(propertyInfo => $"{propertyInfo.Name} = :{propertyInfo.Name}"));
		}
	}

}
