using MediatR;
using Sample.Application.Interfaces;

namespace Sample.Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ITransactionScope _transactionScope;

    public TransactionBehavior(ITransactionScope transactionScope)
    {
        _transactionScope = transactionScope;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request!.GetType().Name.Contains("Command"))
        {
            try
            {

                var response = await next();

                _transactionScope.Commit();

                return response;
            }
            catch (Exception)
            {
                _transactionScope.Rollback();
                throw;
            }
            finally
            {
                _transactionScope.Dispose();
            }
        }
        else
        {
            var response = await next();

            _transactionScope.Dispose();

            return response;
        }
    }
}
