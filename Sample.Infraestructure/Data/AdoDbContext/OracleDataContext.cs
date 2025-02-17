using Oracle.ManagedDataAccess.Client;

namespace Sample.Infraestructure.Data.AdoDbContext
{
    public class OracleDataContext(OracleConnection connection, OracleTransaction transaction) : IDisposable
    {
        private readonly OracleConnection _connection = connection;
        private OracleTransaction? _transaction = transaction;

        public OracleCommand CreateCommand(string sqlQuery)
        {
            OracleCommand command = _connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.Transaction = _transaction;
            return command;
        }

        public void BeginTransaction()
        {
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
            _connection?.Dispose();
        }
    }
}
