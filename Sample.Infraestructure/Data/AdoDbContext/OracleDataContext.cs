using Oracle.ManagedDataAccess.Client;
using Sample.Application.Interfaces;

namespace Sample.Infraestructure.Data.AdoDbContext
{
    public class OracleDataContext(OracleConnection connection) : ITransactionScope
    {
        private readonly OracleConnection _connection = connection;
        private OracleTransaction? _transaction;

        public OracleCommand CreateCommand(string sqlQuery)
		{
			OracleCommand command = _connection.CreateCommand();
			command.CommandText = sqlQuery;
			return command;
		}
        public bool GetConnection()
        {
            return _connection.State == System.Data.ConnectionState.Open;
        }

		public void BeginTransaction()
        {
			if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
			_connection.Open();
			_transaction = _connection.BeginTransaction();
		}

        public void Commit()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Close();
        }
    }
}
