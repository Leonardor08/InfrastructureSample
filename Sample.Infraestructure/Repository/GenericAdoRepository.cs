using Oracle.ManagedDataAccess.Client;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Infraestructure.Data.AdoDbContext;
using System.Data;
using System.Reflection;

namespace Sample.Infraestructure.Repository;

public class GenericAdoRepository<T>(OracleDataContext context) : IAdoRepository<T> where T : Entity
{
	private readonly OracleDataContext _context = context;
	private readonly string _tableName = typeof(T).Name;

	public async Task CreateAsync(T entity)
	{
		try
		{
			_context.BeginTransaction();

			entity.CreatedDate = DateTime.Now;
			entity.Id = Guid.NewGuid();
			// Construir la consulta con parámetros
			string columnNames = string.Join(", ", typeof(T).GetProperties().Select(p => p.Name));
			string paramNames = GetParameters(entity);

			string insertQuery = $"INSERT INTO {_tableName} ({columnNames}) VALUES ({paramNames})";

			using OracleCommand command = _context.CreateCommand(insertQuery);

			await command.ExecuteNonQueryAsync();

			_context.Commit();
		}
		catch (Exception)
		{
			_context.Rollback();
			throw;
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
				list.Add(MapEntity(reader));
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
				entity = (MapEntity(reader));
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
		return entity!;
	}
	public async Task UpdateAsync(T entity, string pKProperty, Guid id)
	{
		try
		{
			_context.BeginTransaction();

			entity.UpdateDate = DateTime.Now;

			string updateQuery = $"UPDATE {_tableName} SET {MapSetClause(entity)} WHERE {pKProperty} = '{id}'";

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

			string deleteQuery = $"DELETE FROM {_tableName} WHERE {property} = '{id}'";

			using OracleCommand command = _context.CreateCommand(deleteQuery);

			await command.ExecuteNonQueryAsync();

			_context.Commit();

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
	public async Task<Dictionary<string, object>> ExcecuteProcedureOutputParams(string package, Dictionary<string, object>? @params = null, Dictionary<string, OracleDbType>? outputParams = null)
	{
		Dictionary<string, object> outputResults = [];

		try
		{
			_context.BeginTransaction();
			using OracleCommand command = _context.CreateCommand(package);

			command.CommandType = CommandType.StoredProcedure;

			if (@params != null)
				foreach (var param in @params)
					command.Parameters.Add(param.Key, param.Value);

			if (outputParams != null)
			{
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
			}

			using OracleDataReader reader = await command.ExecuteReaderAsync();

			if (outputParams != null)
			{
				foreach (var param in outputParams.Keys)
				{
					outputResults[param] = command.Parameters[param].Value;
				}
			}
			Console.WriteLine(outputResults);
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

		return outputResults;
	}
	public async Task<List<T>> ExecuteStoredProcedureWithCursorAsync<T>(
	string procedureName) where T : new()
	{
		List<T> resultSet = [];

		try
		{
			_context.BeginTransaction();
			using OracleCommand command = _context.CreateCommand(procedureName);
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
		return resultSet;
	}
	public async Task<T> ExecuteFunctionAsync<T>(string functionName, Dictionary<string, object> parameters)
	{
		try
		{
			_context.BeginTransaction();
			using OracleCommand command = _context.CreateCommand(functionName);
			command.CommandType = CommandType.Text; 
			string paramList = string.Join(", ", parameters.Keys.Select(k => ":" + k));
			command.CommandText = $"SELECT {functionName}({paramList}) FROM DUAL";

			foreach (var param in parameters)
			{
				command.Parameters.Add(new OracleParameter(param.Key, param.Value));
			}

			object? result = await command.ExecuteScalarAsync();
			return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
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
	public async Task<List<T>> ExecuteViewAsync<T>(string viewName) where T : new()
	{
		List<T> resultSet = [];
		try
		{
			_context.BeginTransaction();
			using OracleCommand command = _context.CreateCommand($"SELECT * FROM {viewName}");
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

		return resultSet;
	}
	private static string GetParameters(T entity)
	{

		List<object> values = [];
		PropertyInfo[] properties = typeof(T).GetProperties();

		foreach (var item in properties)
		{
			Object? value = item.GetValue(entity) ?? null;
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
				properties[i].SetValue(entity, null);

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
	private static string MapSetClause(T entity)
	{
		PropertyInfo[] properties = typeof(T).GetProperties();
		return string.Join(" ,", properties.Select(propertyInfo => $"{propertyInfo.Name} = '{propertyInfo.GetValue(entity)}'"));
	}
}

