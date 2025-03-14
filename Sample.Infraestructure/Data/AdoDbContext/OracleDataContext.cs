using Oracle.ManagedDataAccess.Client;
using Sample.Domain.Interfaces;

namespace Sample.Infraestructure.Data.AdoDbContext
{
    public class OracleDataContext : ITransactionScope
    {
        private readonly OracleConnection _connection;
        private OracleTransaction? _transaction;

        public OracleDataContext(OracleConnection connection) 
            => _connection = connection;

		public OracleCommand CreateCommand(string sqlQuery)
		{
			OracleCommand command = _connection.CreateCommand();
			command.CommandText = sqlQuery;
			return command;
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
