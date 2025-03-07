namespace Sample.Domain.Interfaces;

public interface ITransactionScope
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    void Dispose();
}
